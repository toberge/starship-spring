using UnityEngine;

public class BlockLayout : MonoBehaviour
{
    public delegate void BlockEvent(Direction side);
    public BlockEvent OnSideDeath;

    [SerializeField]
    private Hitbox up;

    [SerializeField]
    private Hitbox down;

    [SerializeField]
    private Hitbox left;

    [SerializeField]
    private Hitbox right;

    private Direction facingSide;
    public Direction FacingSide => facingSide;

    private void Start()
    {
        facingSide = Arena.RelativeDirectionIntoArena(transform.position);
        if (up) up.OnDeath += OnUpDeath;
        if (down) down.OnDeath += OnDownDeath;
        if (left) left.OnDeath += OnLeftDeath;
        if (right) right.OnDeath += OnRightDeath;
    }

    private void OnDestroy()
    {
        if (up) up.OnDeath -= OnUpDeath;
        if (down) down.OnDeath -= OnDownDeath;
        if (left) left.OnDeath -= OnLeftDeath;
        if (right) right.OnDeath -= OnRightDeath;
    }

    public bool IsBlocked(Direction side)
    {
        switch (side)
        {
            case Direction.UP:
                return up && !up.IsDead;
            case Direction.DOWN:
                return down && !down.IsDead;
            case Direction.LEFT:
                return left && !left.IsDead;
            case Direction.RIGHT:
                return right && !right.IsDead;
            default:
                return false;
        }
    }

    private void OnUpDeath(float damage, float remainingHealth)
    {
        OnSideDeath.Invoke(Direction.UP);
    }
    private void OnDownDeath(float damage, float remainingHealth)
    {
        OnSideDeath.Invoke(Direction.DOWN);
    }
    private void OnLeftDeath(float damage, float remainingHealth)
    {
        OnSideDeath.Invoke(Direction.LEFT);
    }
    private void OnRightDeath(float damage, float remainingHealth)
    {
        OnSideDeath.Invoke(Direction.RIGHT);
    }
}
