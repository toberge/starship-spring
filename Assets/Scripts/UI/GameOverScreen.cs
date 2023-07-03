using System.Collections;
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
    private bool restarted;

    private void Start()
    {
        startTime = Time.time;
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        FindFirstObjectByType<Ship>().OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        restartInstructons.transform.parent.LeanScale(Vector3.one * 1.3f, .3f).setEasePunch();
        canvas.enabled = true;
    }

    private void Update()
    {
        var hasGamepad = Gamepad.current != null;
        restartInstructons.text = hasGamepad ? "Press START on your gamepad to retry" : "Press SPACE on your keyboard to retry";
        // TODO any gamepad button
        var gamepadPressed = hasGamepad ? Gamepad.current.startButton.wasPressedThisFrame : false;
        var keyboardPressed = Keyboard.current != null ? Keyboard.current.spaceKey.wasPressedThisFrame : false;
        if (!restarted && canvas.enabled && (gamepadPressed || keyboardPressed))
        {
            restarted = true;
            restartInstructons.transform.parent.LeanScale(Vector3.zero, .2f).setEaseOutCubic().setOnComplete(() =>
            {
                var scene = SceneManager.GetActiveScene();
                SceneManager.LoadSceneAsync(scene.buildIndex);
            });
        }
    }
}
