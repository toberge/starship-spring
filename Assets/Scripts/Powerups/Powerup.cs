using System.Collections;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private LayerMask playerLayer;

    [SerializeField]
    private MeshRenderer shell;

    [SerializeField]
    private Transform content;

    [SerializeField]
    private float duration = 8;

    private PowerupEffect effect;
    private AudioSource audioSource;

    private Material material;

    private void Start()
    {
        effect = GetComponent<PowerupEffect>();
        audioSource = GetComponent<AudioSource>();

        material = Instantiate(shell.material);
        shell.material = material;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((playerLayer.value & (1 << other.gameObject.layer)) == 0)
            return;

        if (other.transform.parent.TryGetComponent<Ship>(out var ship))
        {
            Apply(ship);
        }
    }

    private void Apply(Ship ship)
    {
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<Collider2D>().enabled = false;

        audioSource?.Play();
        var sequence = LeanTween.sequence();
        sequence.append(LeanTween.scale(shell.gameObject, 3f * Vector3.one, 0.2f));
        sequence.append(LeanTween.value(shell.gameObject, SetAlpha, 1, 0, 0.1f)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnComplete(() => Destroy(shell.gameObject)));
        LeanTween.scale(content.gameObject, Vector3.zero, 0.2f).setOnComplete(() => Destroy(content.gameObject));

        effect.Apply(ship);
        StartCoroutine(WearOffEffect(ship));
    }

    private void SetAlpha(float alpha)
    {
        material.SetFloat("_Alpha", alpha);
    }

    private IEnumerator WearOffEffect(Ship ship)
    {
        // Mask that one weird bug where the shell or content lingers
        yield return new WaitForSecondsRealtime(0.31f);
        if (shell) Destroy(shell.gameObject);
        if (content) Destroy(content.gameObject);
        yield return new WaitForSecondsRealtime(duration - 0.31f);
        effect.Remove(ship);
        Destroy(gameObject);
    }
}
