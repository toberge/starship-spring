using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D body;
    private Hitbox hitbox;
    private Cannon cannon;

    [SerializeField]
    private float moveSpeed = 5;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<Hitbox>();
        hitbox.OnDeath += (damage, remainingHealth) => Destroy(gameObject);
        hitbox.OnHit += (damage, remainingHealth) => Debug.Log($"Enemy took {damage} damage");
        cannon = GetComponent<Cannon>();
        cannon.enabled = false;
    }

    void Update()
    {
        var x = Mathf.Abs(transform.position.x);
        var y = Mathf.Abs(transform.position.y);
        if (x < Arena.EnemyHalfWidth && y < Arena.EnemyHalfHeight)
        {
            cannon.enabled = true;
        }
        else if (x > Arena.EnemyHalfWidth)
        {
            transform.position += Mathf.Sign(transform.position.x) * Vector3.left * Time.deltaTime * moveSpeed;
        }
        else
        {
            transform.position += Mathf.Sign(transform.position.y) * Vector3.down * Time.deltaTime * moveSpeed;
        }
    }
}
