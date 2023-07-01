using UnityEngine;

// TODO generalize ContinuousDamage maybe
public class InstantDamage : MonoBehaviour
{
    public delegate void KillEvent();
    public KillEvent OnKill;

    [SerializeField]
    private LayerMask damagedLayers;

    [SerializeField]
    private float damage = 100;

    [SerializeField]
    private float effectDelay = .1f;

    private float lastDisplayedEffectTime;

    private AudioSource audioSource;
    private Flash flash;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        flash = GetComponent<Flash>();
    }

    private bool CanDamage(Collider2D other)
    {
        return (damagedLayers.value & (1 << other.gameObject.layer)) > 0;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (CanDamage(other) && other.gameObject.TryGetComponent<Hitbox>(out var hitbox))
        {
            DamageIfPossible(hitbox);
        }
    }

    private void DamageIfPossible(Hitbox hitbox)
    {
        var remainingHealth = hitbox.Damage(damage);
        if (audioSource && Time.time - lastDisplayedEffectTime > effectDelay)
        {
            if (audioSource != null)
            {
                audioSource?.Play();
            }
            if (flash != null)
            {
                flash.Play();
            }
            lastDisplayedEffectTime = Time.time;
        }
        if (remainingHealth <= 0)
        {
            OnKill?.Invoke();
        }
    }
}
