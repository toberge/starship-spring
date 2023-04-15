using UnityEngine;
using UnityEngine.InputSystem;

public class Ship : MonoBehaviour
{
    public delegate void ShipEvent();
    public ShipEvent OnDeath;

    [SerializeField]
    private Rigidbody2D leftSide;
    private Hitbox leftHitbox;

    [SerializeField]
    private Rigidbody2D rightSide;
    private Hitbox rightHitbox;

    [SerializeField]
    private ContinuousDamage spring;

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
        hitSound = GetComponent<AudioSource>();

        spring.OnKill += OnKill;
        leftHitbox.OnHit += OnHit;
        rightHitbox.OnHit += OnHit;
        leftHitbox.OnDeath += HandleDeath;
        rightHitbox.OnDeath += HandleDeath;
    }

    private void OnHit(float damage, float remainingHealth)
    {
        hitSound.Play();
    }

    private void OnKill()
    {
        var leftHealth = leftHitbox.Heal(killHealAmount);
        var rightHealth = rightHitbox.Heal(killHealAmount);
        kills++;
    }

    private void HandleDeath(float damage, float remainingHealth)
    {
        OnDeath?.Invoke();
        Instantiate(explosion, leftSide.position, Quaternion.identity, transform.parent);
        Instantiate(explosion, rightSide.position, Quaternion.identity, transform.parent);
        Instantiate(explosion, leftSide.position + (rightSide.position - leftSide.position) / 2, Quaternion.identity, transform.parent);
        Destroy(gameObject);
    }

    float f(bool x)
    {
        return x ? 1 : 0;
    }

    // Update is called once per frame
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

        leftSide.AddForce(leftMovement * moveForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
        leftSide.GetComponent<ThrusterBlock>().SetThrusterIntensity(-leftMovement);
        rightSide.AddForce(rightMovement * moveForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
        rightSide.GetComponent<ThrusterBlock>().SetThrusterIntensity(-rightMovement);
    }
}
