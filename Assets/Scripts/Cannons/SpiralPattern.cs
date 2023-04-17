using UnityEngine;

public class SpiralPattern : Pattern
{
    [SerializeField]
    private Bullet bullet;

    [SerializeField]
    private float startSpeed = 3;

    [SerializeField]
    private float acceleration = .1f;

    [SerializeField]
    private float anglesPerSecond = 10;

    public override void Fire(float time, Vector3 direction)
    {
        float angle = time * anglesPerSecond;
        var launchDirection = Quaternion.AngleAxis(angle, Vector3.forward) * direction;
        var launchedBullet = Instantiate(bullet, transform.position, transform.rotation);
        launchedBullet.Motion = new StraightMotion(transform.position, launchDirection, startSpeed, acceleration);
    }
}
