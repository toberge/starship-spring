using UnityEngine;

public class Shield : MonoBehaviour
{
    private Vector3 fullScale;
    private LTDescr tween;

    private void Start()
    {
        fullScale = transform.localScale;
        transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }

    public void Raise()
    {
        gameObject.SetActive(true);
        if (tween != null) LeanTween.cancel(tween.id);
        gameObject.LeanScale(fullScale, .3f).setEaseInCubic();
    }

    public void Lower()
    {
        tween = gameObject.LeanScale(Vector3.zero, .5f)
            .setEaseInCubic()
            .setOnComplete(() => gameObject.SetActive(false));
    }
}
