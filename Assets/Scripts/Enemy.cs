using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Hitbox>().OnDeath += (damage, remainingHealth) => Destroy(gameObject);
        GetComponent<Hitbox>().OnHit += (damage, remainingHealth) => Debug.Log($"Enemy took {damage} damage");
    }
}
