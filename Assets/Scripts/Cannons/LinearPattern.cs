using UnityEngine;

public class LinearPattern : Pattern
{
    [SerializeField]
    private Bullet bullet;

    [SerializeField]
    private float startSpeed;

    [SerializeField]
    private float acceleration;

    private void LaunchBullet(Bullet bullet, Vector3 direction)
    {
        var launchedBullet = Instantiate(bullet, transform.position, transform.rotation);
        launchedBullet.Motion = new StraightMotion(transform.position, direction, startSpeed, acceleration);
    }

    public override void Fire(float time, Vector3 direction)
    {
        LaunchBullet(bullet, direction);
    }
}
