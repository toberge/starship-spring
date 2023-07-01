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
    private InstantDamage leftShield;
    private LTDescr leftShieldTween;

    [SerializeField]
    private InstantDamage rightShield;
    private LTDescr rightShieldTween;

    [SerializeField]
    private InstantDamage spring;

    [SerializeField]
    private GameObject explosion;


    [SerializeField]
    private float moveForce = 10;

    [SerializeField]
    private float killHealAmount = 20;

    private int kills = 0;
    public int Kills => kills;

    private AudioSource hitSound;

    private void Start()
    {
        leftHitbox = leftSide.GetComponent<Hitbox>();
        rightHitbox = rightSide.GetComponent<Hitbox>();
        leftThruster = leftSide.GetComponent<ThrusterBlock>();
        rightThruster = rightSide.GetComponent<ThrusterBlock>();
        hitSound = GetComponent<AudioSource>();

        spring.OnKill += HandleKill;
        leftShield.OnKill += HandleKill;
        rightShield.OnKill += HandleKill;

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
        // TODO kill count is moved
        kills++;
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

    public void ShieldUp()
    {
        // TODO make animation part of a Shield component?
        leftShield.gameObject.SetActive(true);
        rightShield.gameObject.SetActive(true);
        if (leftShieldTween != null) LeanTween.cancel(leftShieldTween.id);
        if (rightShieldTween != null) LeanTween.cancel(rightShieldTween.id);
        leftShield.gameObject.LeanScale(2.5f * Vector3.one, .3f).setEaseInCubic();
        rightShield.gameObject.LeanScale(2.5f * Vector3.one, .3f).setEaseInCubic();
        leftHitbox.enabled = false;
        rightHitbox.enabled = false;
        // TODO disable collisions with enemies maybe?
    }

    public void ShieldDown()
    {
        leftShieldTween = leftShield.gameObject.LeanScale(Vector3.zero, .5f)
            .setEaseInCubic()
            .setOnComplete(() => leftShield.gameObject.SetActive(false));
        rightShieldTween = rightShield.gameObject.LeanScale(Vector3.zero, .5f)
            .setEaseInCubic()
            .setOnComplete(() => rightShield.gameObject.SetActive(false));
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
