using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D body;
    private Hitbox hitbox;
    private Cannon cannon;
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
        cannon = GetComponent<Cannon>();
        cannon.enabled = false;
    }

    private void OnDeath(float damage, float remainingHealth)
    {
        Instantiate(explosion, transform.position, transform.rotation, transform.parent);
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        var x = Mathf.Abs(transform.position.x);
        var y = Mathf.Abs(transform.position.y);
        if (x < Arena.EnemyHalfWidth && y < Arena.EnemyHalfHeight)
        {
            cannon.enabled = true;
        }
        else if (x > Arena.EnemyHalfWidth)
        {
            thruster.AddForce(Mathf.Sign(transform.position.x) * Vector3.left, Time.fixedDeltaTime * moveForce);
        }
        else
        {
            thruster.AddForce(Mathf.Sign(transform.position.y) * Vector3.down, Time.fixedDeltaTime * moveForce);
        }
    }
}
