using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    private float fireDelay = .5f;

    private Pattern pattern;

    private float lastFireTime = 0;

    private Vector3 fireDirection;

    private void Start()
    {
        pattern = GetComponent<Pattern>();
        fireDirection = Arena.DirectionIntoArena(transform.position);
    }

    void Update()
    {
        if (Time.time - lastFireTime >= fireDelay && Arena.IsInFiringRange(transform.position))
        {
            pattern.Fire(Time.time, fireDirection);
            lastFireTime = Time.time;
        }
        // TODO change direction if/when the ship turns around
    }
}
