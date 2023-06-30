using UnityEngine;

public class Enemy : MonoBehaviour
{
    private ThrusterBlock thruster;

    [SerializeField]
    private float moveForce = 1;

    private void Start()
    {
        thruster = GetComponent<ThrusterBlock>();
    }

    void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.x) > Arena.EnemyHalfWidth)
        {
            thruster.AddForce(Mathf.Sign(transform.position.x) * Vector3.left, Time.fixedDeltaTime * moveForce);
        }
        else if (Mathf.Abs(transform.position.y) > Arena.EnemyHalfHeight)
        {
            thruster.AddForce(Mathf.Sign(transform.position.y) * Vector3.down, Time.fixedDeltaTime * moveForce);
        }
        else
        {
            thruster.Stop();
        }
    }
}
