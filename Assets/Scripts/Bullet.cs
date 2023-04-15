using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Color color1;

    [SerializeField]
    private Color color2;

    [SerializeField]
    private AnimationCurve pulseCurve;

    private SpriteRenderer spriteRenderer;

    public Pattern Pattern;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        var color = Color.Lerp(color1, color2, pulseCurve.Evaluate(Time.time));
        spriteRenderer.color = color;

        transform.position = Pattern.PositionAt(Time.time);
    }
}
