using UnityEngine;

public class HomingMotion : Motion
{
    private Vector3 position;
    private Vector3 direction;
    private float startSpeed;
    private float acceleration;
    private AnimationCurve homingFactorCurve;
    private float homingTaperTime;
    private float startTime;
    private float lastUpdateTime;
    private Transform target;

    public HomingMotion(Vector3 startPosition, Vector3 direction, Transform target, float startSpeed, float acceleration, AnimationCurve homingFactorCurve, float homingTaperTime)
    {
        this.position = startPosition;
        this.direction = direction;
        this.startSpeed = startSpeed;
        this.acceleration = acceleration;
        this.homingFactorCurve = homingFactorCurve;
        this.homingTaperTime = homingTaperTime;
        this.startTime = Time.time;
        this.lastUpdateTime = Time.time;
        this.target = target;
    }

    public Vector3 PositionAt(float launchTime)
    {
        var t = launchTime - startTime;
        // TODO this should be Time.deltaTime
        var dt = launchTime - lastUpdateTime;
        lastUpdateTime = launchTime;
        if (target)
        {
            var perfectDirection = (target.position - position).normalized;
            var homingFactor = homingFactorCurve.Evaluate(t / homingTaperTime);
            direction = Vector3.Slerp(direction, perfectDirection, homingFactor);
        }
        return position += direction * (startSpeed + acceleration * t) * dt;
    }
}
