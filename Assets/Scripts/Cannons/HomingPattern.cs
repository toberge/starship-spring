using UnityEngine;

public class HomingPattern : Pattern
{
    [SerializeField]
    private Bullet bullet;

    [SerializeField]
    private float startSpeed;

    [SerializeField]
    private float acceleration;

    [SerializeField]
    private AnimationCurve homingFactorCurve;

    [SerializeField]
    private float homingTaperTime = 4;

    private Ship ship;

    private void Start()
    {
        ship = FindFirstObjectByType<Ship>();
    }

    private void LaunchBullet(Bullet bullet, Vector3 direction)
    {
        var launchedBullet = Instantiate(bullet, transform.position, transform.rotation);
        var target = Random.Range(0, 1) == 1 ? ship.LeftSide : ship.RightSide;
        launchedBullet.Motion = new HomingMotion(transform.position, direction, target, startSpeed, acceleration, homingFactorCurve, homingTaperTime);
    }

    public override void Fire(float time, Vector3 direction)
    {
        LaunchBullet(bullet, direction);
    }
}
