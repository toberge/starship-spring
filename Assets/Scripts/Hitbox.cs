using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public delegate void HitEvent(float damage, float remainingHealth);
    public HitEvent OnHit, OnDeath;

    [SerializeField]
    private float maxHealth = 100;

    private float health;

    void Start()
    {
        health = maxHealth;
    }

    public void Damage(float damage)
    {
        health = Mathf.Clamp(health - damage, 0, maxHealth);
        OnHit?.Invoke(damage, health);
        if (health == 0)
        {
            OnDeath?.Invoke(damage, health);
        }
    }
}
