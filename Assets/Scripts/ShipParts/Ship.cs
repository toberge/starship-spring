using UnityEngine;
using UnityEngine.InputSystem;

public class Ship : MonoBehaviour
{
    public delegate void ShipEvent();
    public ShipEvent OnDeath;
    public ShipEvent OnKill;

    [SerializeField]
    private Rigidbody2D leftSide;
    private Hitbox leftHitbox;
    private ThrusterBlock leftThruster;
    public Transform LeftSide => leftSide.transform;

    [SerializeField]
    private Rigidbody2D rightSide;
    private Hitbox rightHitbox;
    private ThrusterBlock rightThruster;
    public Transform RightSide => rightSide.transform;

    [SerializeField]
    private Shield leftShield;
    private LTDescr leftShieldTween;

    [SerializeField]
    private Shield rightShield;
    private LTDescr rightShieldTween;

    [SerializeField]
    private InstantDamage spring;

    [SerializeField]
    private GameObject explosion;


    [SerializeField]
    private float moveForce = 10;

    [SerializeField]
    private float killHealAmount = 20;

    private AudioSource hitSound;

    private void Start()
    {
        leftHitbox = leftSide.GetComponent<Hitbox>();
        rightHitbox = rightSide.GetComponent<Hitbox>();
        leftThruster = leftSide.GetComponent<ThrusterBlock>();
        rightThruster = rightSide.GetComponent<ThrusterBlock>();
        hitSound = GetComponent<AudioSource>();

        spring.OnKill += HandleKill;
        leftShield.GetComponent<InstantDamage>().OnKill += HandleKill;
        rightShield.GetComponent<InstantDamage>().OnKill += HandleKill;

        leftHitbox.OnHit += OnHit;
        rightHitbox.OnHit += OnHit;

        leftHitbox.OnDeath += HandleDeath;
        rightHitbox.OnDeath += HandleDeath;
    }

    private void OnHit(float damage, float remainingHealth)
    {
        hitSound.Play();
    }

    private void HandleKill()
    {
        var leftHealth = leftHitbox.Heal(killHealAmount);
        var rightHealth = rightHitbox.Heal(killHealAmount);
        OnKill?.Invoke();
    }

    private void HandleDeath(float damage, float remainingHealth)
    {
        OnDeath?.Invoke();
        Instantiate(explosion, leftSide.position, Quaternion.identity, transform.parent);
        Instantiate(explosion, rightSide.position, Quaternion.identity, transform.parent);
        Instantiate(explosion, leftSide.position + (rightSide.position - leftSide.position) / 2, Quaternion.identity, transform.parent);
        Destroy(gameObject);
    }

    public void RaiseShields()
    {
        leftShield.Raise();
        rightShield.Raise();
        leftHitbox.enabled = false;
        rightHitbox.enabled = false;
        // TODO disable collisions with enemies maybe?
    }

    public void LowerShields()
    {
        leftShield.Lower();
        rightShield.Lower();
        leftHitbox.enabled = true;
        rightHitbox.enabled = true;
    }

    private float f(bool x)
    {
        return x ? 1 : 0;
    }

    private void FixedUpdate()
    {
        var gamepad = Gamepad.current;
        var keyboard = Keyboard.current;
        Vector2 leftMovement, rightMovement;

        if (gamepad == null)
        {
            leftMovement = new Vector2(f(keyboard.dKey.isPressed) - f(keyboard.aKey.isPressed), f(keyboard.wKey.isPressed) - f(keyboard.sKey.isPressed));
            rightMovement = new Vector2(f(keyboard.lKey.isPressed) - f(keyboard.jKey.isPressed), f(keyboard.iKey.isPressed) - f(keyboard.kKey.isPressed));
        }
        else
        {
            leftMovement = gamepad.leftStick.ReadValue();
            rightMovement = gamepad.rightStick.ReadValue();
        }

        leftThruster.AddForce(leftMovement, moveForce * Time.fixedDeltaTime);
        rightThruster.AddForce(rightMovement, moveForce * Time.fixedDeltaTime);
    }
}
