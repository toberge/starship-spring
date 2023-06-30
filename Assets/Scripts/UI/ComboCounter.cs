using TMPro;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct ComboRating
{
    public int threshold;
    public string name;
    public Color color;
}

// TODO calculate score here!
public class ComboCounter : MonoBehaviour
{
    [SerializeField]
    private Ship ship;

    [SerializeField]
    private TMP_Text scoreText;

    [SerializeField]
    private TMP_Text counterText;

    [SerializeField]
    private TMP_Text ratingText;

    // TODO add progress bar for combo timeout maybe
    [SerializeField]
    private float comboTimeout = 3f;

    [SerializeField]
    private float popInTime = .5f;

    [SerializeField]
    private ComboRating[] ratings;
    private int rating = -1;

    private int combo = 0;
    private float lastKillTime;

    private void Start()
    {
        ship.OnKill += OnKill;
        counterText.text = "";
        ratingText.text = "";
    }

    private void OnKill()
    {
        lastKillTime = Time.time;
        combo++;
        if (combo <= 1) return;

        counterText.text = $"{combo}x combo";

        // Check if we have a better rating
        var newRating = ratings
            .TakeWhile((r) => combo >= r.threshold)
            .Count() - 1;
        if (newRating != rating) {
            // Display it
            rating = newRating;
            ratingText.text = ratings[rating].name;
            ratingText.color = ratings[rating].color;
            // Set a new rotation so we're looking a little skewed
            var rotation = Quaternion.Euler(0, 0, Random.Range(-20,20));
            counterText.transform.rotation = rotation;
            ratingText.transform.rotation = rotation;
        }


        // Pop in
        counterText.transform.localScale = Vector2.one;
        ratingText.transform.localScale = Vector2.one;
        counterText.gameObject
            .LeanScale(Vector3.one * 1.2f, popInTime)
            .setEasePunch();
        ratingText.gameObject
            .LeanScale(Vector3.one * 1.3f, popInTime)
            .setEasePunch();
    }

    private void Update()
    {
        if (combo > 0 && Time.time - lastKillTime > comboTimeout)
        {
            combo = 0;
            rating = -1;

            // Pop out
            counterText.gameObject.LeanScale(Vector3.zero, popInTime);
            ratingText.gameObject.LeanScale(Vector3.zero, popInTime);
        }
    }
}
