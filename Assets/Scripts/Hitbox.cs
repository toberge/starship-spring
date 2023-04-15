using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public delegate void HitEvent(float damage, float remainingHealth);
    public HitEvent OnHit, OnDeath, OnHeal;

    [SerializeField]
    private float maxHealth = 100;
    public float MaxHealth => maxHealth;

    private float health;

    private void Start()
    {
        health = maxHealth;
    }

    public float Damage(float damage)
    {
        health = Mathf.Clamp(health - damage, 0, maxHealth);
        OnHit?.Invoke(damage, health);
        if (health == 0)
        {
            OnDeath?.Invoke(damage, health);
        }
        return health;
    }

    public float Heal(float amount)
    {
        health = Mathf.Clamp(health + amount, 0, maxHealth);
        OnHeal?.Invoke(amount, health);
        return health;
    }
}
