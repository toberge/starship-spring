using UnityEngine;

public class CrossPattern : Pattern
{
    [SerializeField]
    private Bullet upBullet;

    [SerializeField]
    private Bullet downBullet;

    [SerializeField]
    private Bullet leftBullet;

    [SerializeField]
    private Bullet rightBullet;

    [SerializeField]
    private float startSpeed;
    [SerializeField]
    private float acceleration;

    private void LaunchBullet(Bullet bullet, Vector3 direction)
    {
        var launchedBullet = Instantiate(bullet, transform.position, transform.rotation);
        launchedBullet.Motion = new Straight(transform.position, direction, startSpeed, acceleration);
    }

    public override void Fire(float time)
    {
        LaunchBullet(upBullet, transform.up);
        LaunchBullet(downBullet, -transform.up);
        LaunchBullet(leftBullet, -transform.right);
        LaunchBullet(rightBullet, transform.right);
    }
}
