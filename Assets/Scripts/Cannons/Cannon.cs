using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    private float fireDelay = .5f;

    private Pattern pattern;

    private float firstFireTime = -1;
    private float lastFireTime = 0;

    private Vector3 fireDirection;

    private Direction blockedDirection;

    private BlockLayout layout;

    private bool isFiring;

    private void Start()
    {
        pattern = GetComponent<Pattern>();
        layout = GetComponent<BlockLayout>();
        // Invert rotation so TransformDirection works without issue in the future (when the ship is rotated)
        fireDirection = Quaternion.Inverse(transform.rotation) * Arena.VectorIntoArena(transform.position);
        blockedDirection = Arena.RelativeDirectionIntoArena(transform.position);
        isFiring = !layout.IsBlocked(blockedDirection);
        layout.OnSideDeath += OnSideDeath;
    }

    private void OnSideDeath(Direction side)
    {
        if (side == blockedDirection)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        if (isFiring && Time.time - lastFireTime >= fireDelay && Arena.IsInFiringRange(transform.position))
        {
            if (firstFireTime < 0)
            {
                firstFireTime = Time.time;
            }
            pattern.Fire(Time.time - firstFireTime, transform.TransformDirection(fireDirection));
            lastFireTime = Time.time;
        }
        // TODO change direction if/when the ship turns around
    }
}
