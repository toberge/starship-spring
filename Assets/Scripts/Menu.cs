using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private TMP_Text startInstructions;

    [SerializeField]
    private TMP_Text controlInstructions;

    void Update()
    {
        var hasGamepad = Gamepad.current != null;
        startInstructions.text = hasGamepad ? "Press START on your gamepad to start" : "Press SPACE on your keyboard to start";
        controlInstructions.text = hasGamepad ? "Steer your ship with left and right stick" : "Steer your ship with WASD and IJKL";
        var gamepadPressed = hasGamepad ? Gamepad.current.startButton.wasPressedThisFrame : false;
        var keyboardPressed = Keyboard.current != null ? Keyboard.current.spaceKey.wasPressedThisFrame : false;
        if ((gamepadPressed || keyboardPressed))
        {
            SceneManager.LoadSceneAsync("Arena");
        }
    }
}
