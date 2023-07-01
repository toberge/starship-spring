using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringCatcher : MonoBehaviour
{
    [SerializeField]
    private ThrusterBlock leftSide;

    [SerializeField]
    private ThrusterBlock rightSide;

    [SerializeField]
    private float moveForce = 1;

    private Ship ship;
    private Vector3 direction;
    private Transform target;

    private void Start()
    {
        direction = Arena.VectorIntoArena(transform.position);
        ship = FindObjectOfType<Ship>();
        // TODO randomly target either side
        target = ship.LeftSide;
    }

    private void Update()
    {
        if (leftSide && rightSide && target)
        {
            // TODO ensure you rotate properly :(
            var center = rightSide.transform.position + (leftSide.transform.position - rightSide.transform.position) / 2;
            var perfectDirection = (target.position - center).normalized;
            direction = Vector3.Slerp(direction, perfectDirection, 0.95f);
            leftSide.AddForce(direction, moveForce, ForceMode2D.Force);
            rightSide.AddForce(direction, moveForce, ForceMode2D.Force);
        }
        else
        {
            if (leftSide) leftSide.SetThrusterIntensity(Vector2.zero);
            if (rightSide) rightSide.SetThrusterIntensity(Vector2.zero);
            this.enabled = false;
        }
    }
}
