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

    private Rigidbody2D body;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
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

    // Must be called from FixedUpdate
    public void AddForce(Vector2 direction, float force, ForceMode2D mode = ForceMode2D.Impulse)
    {
        body.AddForce(force * direction, mode);
        SetThrusterIntensity(-direction);
    }

    public void Stop()
    {
        SetThrusterIntensity(Vector2.zero);
    }
}
