using UnityEngine;

public class ContinuousDamage : MonoBehaviour
{
    public delegate void KillEvent();
    public KillEvent OnKill;

    [SerializeField]
    private LayerMask damagedLayers;

    [SerializeField]
    private float damage = 10;

    [SerializeField]
    private float damageDelay = .1f;

    private float lastDamageTime;

    bool CanDamage(Collider2D other)
    {
        return (damagedLayers.value & (1 << other.gameObject.layer)) > 0;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (CanDamage(other) && other.gameObject.TryGetComponent<Hitbox>(out var hitbox))
        {
            DamageIfPossible(hitbox);
        }
    }


    private void DamageIfPossible(Hitbox hitbox)
    {
        if (Time.time - lastDamageTime > damageDelay)
        {
            var remainingHealth = hitbox.Damage(damage);
            if (remainingHealth <= 0)
            {
                OnKill?.Invoke();
            }
            lastDamageTime = Time.time;
        }
    }
}
