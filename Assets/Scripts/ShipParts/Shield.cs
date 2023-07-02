using UnityEngine;

public class Shield : MonoBehaviour
{
    private Vector3 fullScale;
    private LTDescr tween;

    private Collider2D shieldCollider;

    private void Start()
    {
        fullScale = transform.localScale;
        transform.localScale = Vector3.zero;
        shieldCollider = GetComponent<Collider2D>();
        shieldCollider.enabled = false;
    }

    public void Raise()
    {
        gameObject.SetActive(true);
        if (tween != null) LeanTween.cancel(tween.id);
        gameObject.LeanScale(fullScale, .3f).setEaseInCubic();
        shieldCollider.enabled = true;
    }

    public void Lower()
    {
        tween = gameObject.LeanScale(Vector3.zero, .5f)
            .setEaseInCubic()
            .setOnComplete(() => shieldCollider.enabled = true);
    }
}
