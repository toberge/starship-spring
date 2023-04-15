using UnityEngine;
using UnityEngine.UI;

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

    void Start()
    {
        leftHitbox.OnHeal += OnLeftHit;
        leftHitbox.OnHit += OnLeftHit;
        rightHitbox.OnHeal += OnRightHit;
        rightHitbox.OnHit += OnRightHit;

        // Ensure we're not writing to the material by instantiating it
        material = GetComponent<Image>().material;
        material = Instantiate(material);
        GetComponent<Image>().material = material;
        SetLeftHealth(leftHitbox.MaxHealth);
        SetRightHealth(rightHitbox.MaxHealth);
    }

    private void OnLeftHit(float delta, float remainingHealth)
    {
        LeanTween.value(gameObject, SetLeftHealth, leftHealth, remainingHealth, transitionTime);
        leftHealth = remainingHealth;
    }

    private void OnRightHit(float delta, float remainingHealth)
    {
        LeanTween.value(gameObject, SetRightHealth, rightHealth, remainingHealth, transitionTime);
        rightHealth = remainingHealth;
    }

    private void SetLeftHealth(float health)
    {
        material.SetFloat("_LeftHealth", health / leftHitbox.MaxHealth);
    }

    private void SetRightHealth(float health)
    {
        material.SetFloat("_RightHealth", health / rightHitbox.MaxHealth);
    }
}
