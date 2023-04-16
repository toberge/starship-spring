using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Quit : MonoBehaviour
{
    void Update()
    {
        if ((Gamepad.current != null && Gamepad.current.selectButton.isPressed) || Keyboard.current.escapeKey.isPressed)
        {
            Application.Quit();
        }
    }
}
