using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private LayerMask playerLayer;

    [SerializeField]
    private MeshRenderer shell;

    [SerializeField]
    private MeshRenderer spring;

    [SerializeField]
    private float duration = 8;

    private PowerupEffect effect;
    private AudioSource audioSource;

    private Material material;

    private void Start()
    {
        effect = GetComponent<PowerupEffect>();
        audioSource = GetComponent<AudioSource>();

        material = shell.material;
        material = Instantiate(material);
        shell.material = material;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // TODO alternatively do it from component on ship
        //      (overlapping times make this kinda thing necessary)
        if ((playerLayer.value & (1 << other.gameObject.layer)) == 0)
            return;

        if (other.transform.parent.TryGetComponent<Ship>(out var ship))
        {
            Apply(ship);
        }
    }

    private void Apply(Ship ship)
    {
            audioSource?.Play();
            shell.gameObject.LeanScale(3f * Vector3.one, 0.2f);
            LeanTween.value(shell.gameObject, SetAlpha, 1, 0, 0.25f).setEase(LeanTweenType.easeOutQuad).setOnComplete(() => Destroy(shell.gameObject));
            spring.gameObject.LeanScale(Vector3.zero, 0.2f).setOnComplete(() => Destroy(spring.gameObject));

            effect.Apply(ship);
            GetComponent<Rigidbody2D>().simulated = false;
            StartCoroutine(WearOffEffect(ship));
    }

    private void SetAlpha(float alpha)
    {
        material.SetFloat("_Alpha", alpha);
    }

    private IEnumerator WearOffEffect(Ship ship)
    {
        yield return new WaitForSecondsRealtime(duration);
        effect.Remove(ship);
        Destroy(gameObject);
    }
}
