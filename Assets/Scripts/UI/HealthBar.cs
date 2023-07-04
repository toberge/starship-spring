using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Hitbox leftHitbox;

    [SerializeField]
    private Hitbox rightHitbox;

    [SerializeField]
    private float transitionTime = 0.2f;

    private Material material;

    private float leftHealth = 100;
    private float rightHealth = 100;

    private LTDescr leftTween;
    private LTDescr rightTween;

    void Start()
    {
        leftHitbox.OnHeal += OnLeftHit;
        leftHitbox.OnHit += OnLeftHit;
        rightHitbox.OnHeal += OnRightHit;
        rightHitbox.OnHit += OnRightHit;

        // Ensure we're not writing to the material by instantiating it
        material = GetComponent<SpriteRenderer>().material;
        material = Instantiate(material);
        GetComponent<SpriteRenderer>().material = material;

        leftHealth = leftHitbox.MaxHealth;
        rightHealth = rightHitbox.MaxHealth;
        SetLeftHealth(leftHitbox.MaxHealth);
        SetRightHealth(rightHitbox.MaxHealth);
    }

    private void OnLeftHit(float delta, float remainingHealth)
    {
        SetLeftHealth(remainingHealth);
        leftHealth = remainingHealth;
    }

    private void OnRightHit(float delta, float remainingHealth)
    {
        SetRightHealth(remainingHealth);
        rightHealth = remainingHealth;
    }

    private void SetLeftHealth(float health)
    {
        material.SetFloat("_LeftHealth", Mathf.Clamp(health / leftHitbox.MaxHealth, 0.001f, 0.999f));
    }

    private void SetRightHealth(float health)
    {
        material.SetFloat("_RightHealth", Mathf.Clamp(health / rightHitbox.MaxHealth, 0.001f, 0.999f));
    }
}
