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
        if (upBullet) LaunchBullet(upBullet, transform.up);
        if (downBullet) LaunchBullet(downBullet, -transform.up);
        if (leftBullet) LaunchBullet(leftBullet, -transform.right);
        if (rightBullet) LaunchBullet(rightBullet, transform.right);
    }
}
