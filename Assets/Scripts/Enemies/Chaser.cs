using UnityEngine;

internal enum State
{
    CATCHING,
    TURNING,
    CHASING,
    SPEEDING
}

public class Chaser : MonoBehaviour
{
    [SerializeField]
    private ThrusterBlock leftSide;

    [SerializeField]
    private ThrusterBlock rightSide;

    private ThrusterBlock catchingSide;

    [SerializeField]
    private float moveForce = 1;

    [SerializeField]
    private float catchThreshold = 10;

    [SerializeField]
    private float turnThreshold = 20;

    [SerializeField]
    private float turnTime = 6;

    private Ship ship;
    private Transform target;

    private Vector3 direction;
    private State state = State.CHASING;

    private float endOfChaseTime = 0;

    private void Start()
    {
        direction = Arena.VectorIntoArena(transform.position);
        ship = FindObjectOfType<Ship>();

        // Randomly target either side
        target = new Transform[] { ship.LeftSide, ship.RightSide }[Random.Range(0, 2)];

        // Turn if facing down/up initially
        if (Mathf.Abs(Vector3.Dot(direction, Vector3.up)) > 0)
        {
            transform.Rotate(Vector3.forward * 90);
        }
    }

    private void FixedUpdate()
    {
        if (leftSide && rightSide && target)
        {
            Chase();
        }
        else
        {
            if (leftSide) leftSide.SetThrusterIntensity(Vector2.zero);
            if (rightSide) rightSide.SetThrusterIntensity(Vector2.zero);
            this.enabled = false;
        }
    }

    private void Chase()
    {
        var center = rightSide.transform.position + (leftSide.transform.position - rightSide.transform.position) / 2;
        var distance = Vector3.Distance(center, target.position);
        var perfectDirection = (target.position - center).normalized;

        switch (state)
        {
            case State.CATCHING:
                {
                    catchingSide.AddForce(catchingSide.transform.TransformDirection(direction), moveForce * 2, ForceMode2D.Force);
                    if (distance > catchThreshold)
                    {
                        endOfChaseTime = Time.time;
                        state = State.TURNING;
                    }
                    break;
                }
            case State.CHASING:
                {
                    // Head for where the player is
                    // TODO turn each side separately if not aligned
                    direction = Vector3.Slerp(direction, perfectDirection, 0.95f);
                    leftSide.AddForce(direction, moveForce, ForceMode2D.Force);
                    rightSide.AddForce(direction, moveForce, ForceMode2D.Force);
                    if (distance < catchThreshold)
                    {
                        state = State.CATCHING;
                        bool leftIsFurthestAway = Vector3.Distance(leftSide.transform.position, target.position) > Vector3.Distance(rightSide.transform.position, target.position);
                        catchingSide = leftIsFurthestAway ? leftSide : rightSide;
                        (leftIsFurthestAway ? rightSide : leftSide).SetThrusterIntensity(Vector2.zero);
                        // Transform to local space so we keep the same direction
                        direction = catchingSide.transform.InverseTransformDirection(direction);
                    }
                    else if (distance > turnThreshold)
                    {
                        state = State.SPEEDING;
                    }
                    break;
                }
            case State.TURNING:
                {
                    // Head for where the player is
                    direction = Vector3.Slerp(direction, perfectDirection, 0.99f);
                    leftSide.AddForce(direction, moveForce * 4, ForceMode2D.Force);
                    rightSide.AddForce(direction, moveForce * 4, ForceMode2D.Force);
                    if (distance < catchThreshold)
                    {
                        state = State.CHASING;
                    }
                    else if (Time.time - endOfChaseTime > turnTime)
                    {
                        if (distance > turnThreshold)
                        {
                            state = State.SPEEDING;
                        }
                        else
                        {
                            state = State.CHASING;
                        }
                    }
                    break;
                }
            case State.SPEEDING:
            default:
                {
                    // Head for where the player is
                    direction = Vector3.Slerp(direction, perfectDirection, 0.99f);
                    leftSide.AddForce(direction, moveForce * 2, ForceMode2D.Force);
                    rightSide.AddForce(direction, moveForce * 2, ForceMode2D.Force);
                    if (distance < turnThreshold)
                    {
                        state = State.CHASING;
                    }
                    break;
                }
        }
    }
}
