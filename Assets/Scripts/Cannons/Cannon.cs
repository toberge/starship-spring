using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    private float fireRate = .5f;

    private Pattern pattern;

    private float lastFireTime = 0;

    private void Start()
    {
        pattern = GetComponent<Pattern>();
    }

    void Update()
    {
        //var x = Mathf.Abs(transform.position.x);
        //var y = Mathf.Abs(transform.position.y);
        //    if (x<Arena.EnemyHalfWidth && y<Arena.EnemyHalfHeight)
        // if (Time.time - lastFireTime >= fireRate && Arena.IsInFiringRange(transform.position))
        if (Time.time - lastFireTime >= fireRate)
        {
            pattern.Fire(Time.time);
            lastFireTime = Time.time;
        }
    }
}
