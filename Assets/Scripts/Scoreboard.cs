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
        // TODO any gamepad button
        var gamepadPressed = Gamepad.current != null ? Gamepad.current.aButton.wasPressedThisFrame : false;
        var keyboardPressed = Keyboard.current != null ? Keyboard.current.anyKey.wasPressedThisFrame : false;
        if (canvas.enabled && (gamepadPressed || keyboardPressed))
        {
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadSceneAsync(scene.buildIndex);
        }
    }
}
