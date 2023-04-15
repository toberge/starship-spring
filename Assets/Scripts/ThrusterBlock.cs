using UnityEngine;
using UnityEngine.VFX;

public class ThrusterBlock : MonoBehaviour
{
    [SerializeField]
    private VisualEffect up;
    [SerializeField]
    private VisualEffect down;
    [SerializeField]
    private VisualEffect left;
    [SerializeField]
    private VisualEffect right;

    private void Start()
    {
        up.SetFloat("Intensity", 0);
        down.SetFloat("Intensity", 0);
        left.SetFloat("Intensity", 0);
        right.SetFloat("Intensity", 0);
    }

    public void SetThrusterIntensity(Vector2 direction)
    {
        up.SetFloat("Intensity", Vector2.Dot(direction, transform.up));
        down.SetFloat("Intensity", Vector2.Dot(direction, -transform.up));
        left.SetFloat("Intensity", Vector2.Dot(direction, -transform.right));
        right.SetFloat("Intensity", Vector2.Dot(direction, transform.right));
    }
}
