using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Color color1;

    [SerializeField]
    private Color color2;

    [SerializeField]
    private AnimationCurve pulseCurve;

    private float startTime;

    [SerializeField]
    private float damage = 10;

    [SerializeField]
    private LayerMask damagedLayers;

    private SpriteRenderer spriteRenderer;

    public Motion Motion;

    void Start()
    {
        startTime = Time.time;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var isDamaged = (damagedLayers.value & (1 << other.gameObject.layer)) > 0;
        Debug.Log($"layer {damagedLayers.value} & {other.gameObject.layer}");
        if (isDamaged && other.gameObject.TryGetComponent<Hitbox>(out var hitbox))
        {
            hitbox.Damage(damage);
        }
        if (isDamaged)
        {
            // TODO spawn explosion or sth
            Destroy(gameObject);
        }
    }

    void Update()
    {
        var color = Color.Lerp(color1, color2, pulseCurve.Evaluate((Time.time - startTime) * 2));
        spriteRenderer.color = color;

        transform.position = Motion.PositionAt(Time.time);
    }
}
