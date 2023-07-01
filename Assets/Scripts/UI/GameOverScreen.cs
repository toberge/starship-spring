using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Scoreboard : MonoBehaviour
{
    [SerializeField]
    private TMP_Text restartInstructons;

    private float startTime;
    private Canvas canvas;

    private void Start()
    {
        startTime = Time.time;
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        FindFirstObjectByType<Ship>().OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        canvas.enabled = true;
    }

    private void Update()
    {
        var hasGamepad = Gamepad.current != null;
        restartInstructons.text = hasGamepad ? "Press START on your gamepad to retry" : "Press SPACE on your keyboard to retry";
        // TODO any gamepad button
        var gamepadPressed = hasGamepad ? Gamepad.current.startButton.wasPressedThisFrame : false;
        var keyboardPressed = Keyboard.current != null ? Keyboard.current.spaceKey.wasPressedThisFrame : false;
        if (canvas.enabled && (gamepadPressed || keyboardPressed))
        {
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadSceneAsync(scene.buildIndex);
        }
    }
}
