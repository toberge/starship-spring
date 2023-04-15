using UnityEngine;

public class Straight : Pattern
{
    private Vector3 startPosition;
    private Vector3 direction;
    private float startSpeed;
    private float acceleration;
    private float startTime;

    public Straight(Vector3 startPosition, Vector3 direction, float startSpeed, float acceleration)
    {
        this.startPosition = startPosition;
        this.direction = direction;
        this.startSpeed = startSpeed;
        this.acceleration = acceleration;
        this.startTime = Time.time;
    }

    public Vector3 PositionAt(float launchTime)
    {
        var t = launchTime - startTime;
        return startPosition + direction * (startSpeed * t + 0.5f * acceleration * t * t);
    }
}
