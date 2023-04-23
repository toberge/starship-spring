using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D body;
    private Hitbox hitbox;
    private ThrusterBlock thruster;

    [SerializeField]
    private GameObject explosion;

    [SerializeField]
    private float moveForce = 1;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        thruster = GetComponent<ThrusterBlock>();
        hitbox = GetComponent<Hitbox>();
        hitbox.OnDeath += OnDeath;
        hitbox.OnHit += (damage, remainingHealth) => Debug.Log($"Enemy took {damage} damage");
    }

    private void OnDeath(float damage, float remainingHealth)
    {
        Instantiate(explosion, transform.position, transform.rotation, transform.parent);
        Destroy(gameObject);
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
