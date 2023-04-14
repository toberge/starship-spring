using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ship : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D leftSide;

    [SerializeField]
    private Rigidbody2D rightSide;

    [SerializeField]
    private float moveForce = 10;

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
        Debug.Log(leftMovement);

        leftSide.AddForce(leftMovement * moveForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
        leftSide.GetComponent<ThrusterBlock>().SetThrusterIntensity(-leftMovement);
        rightSide.AddForce(rightMovement * moveForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
        rightSide.GetComponent<ThrusterBlock>().SetThrusterIntensity(-rightMovement);

    }
}
