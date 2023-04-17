using UnityEngine;

public class SinePattern : Pattern
{
    [SerializeField]
    private Bullet bullet;

    [SerializeField]
    private float speed = 3;

    [SerializeField]
    private float frequency = 3;

    [SerializeField]
    private float amplitude = 3;

    public override void Fire(float time, Vector3 direction)
    {
        var launchedBullet = Instantiate(bullet, transform.position, transform.rotation);
        launchedBullet.Motion = new SineMotion(transform.position, direction, speed, frequency, amplitude);
    }
}
