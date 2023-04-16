using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve curve;

    [SerializeField]
    private float duration = .2f;

    private Material material;

    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        SetIntensity(0);
    }

    private void SetIntensity(float t)
    {
        material.SetFloat("_Intensity", curve.Evaluate(t));
    }

    public void Play()
    {
        LeanTween.value(gameObject, SetIntensity, 0, 1, duration);
    }
}
