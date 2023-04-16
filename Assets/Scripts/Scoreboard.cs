using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Scoreboard : MonoBehaviour
{
    [SerializeField]
    private Ship ship;

    [SerializeField]
    private TMP_Text scoreText;

    [SerializeField]
    private TMP_Text restartInstructons;

    private float startTime;
    private Canvas canvas;

    // Start is called before the first frame update
    private void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        ship.OnDeath += OnDeath;
    }

    private int Score()
    {
        return Mathf.RoundToInt(20 * (Time.time - startTime)) + 100 * ship.Kills;
    }

    private void OnDeath()
    {
        canvas.enabled = true;
        scoreText.text = Score().ToString();
    }

    private void Update()
    {
        var hasGamepad = Gamepad.current != null;
        restartInstructons.text = hasGamepad ? "Press A on your gamepad to retry" : "Press R on your keyboard to retry";
        // TODO any gamepad button
        var gamepadPressed = hasGamepad ? Gamepad.current.aButton.wasPressedThisFrame : false;
        var keyboardPressed = Keyboard.current != null ? Keyboard.current.rKey.wasPressedThisFrame : false;
        if (canvas.enabled && (gamepadPressed || keyboardPressed))
        {
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadSceneAsync(scene.buildIndex);
        }

    }
}
