using UnityEngine;

public class SineMotion : Motion
{
    private Vector3 startPosition;
    private Vector3 direction;
    private float speed;
    private float frequency;
    private float amplitude;
    private float startTime;
    private Vector3 up;

    public SineMotion(Vector3 startPosition, Vector3 direction, float speed, float frequency, float amplitude)
    {
        this.startPosition = startPosition;
        this.direction = direction;
        this.speed = speed;
        this.frequency = frequency;
        this.amplitude = amplitude;
        this.startTime = Time.time;
        this.up = Vector3.Cross(direction, Vector3.forward);
    }

    public Vector3 PositionAt(float launchTime)
    {
        var t = launchTime - startTime;
        return startPosition + direction * speed * t + up * Mathf.Sin(t * frequency) * amplitude;
    }
}
