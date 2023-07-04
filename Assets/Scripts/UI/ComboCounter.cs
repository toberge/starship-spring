using TMPro;
using System;
using System.Linq;
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

    private int score = 0;
    public int Score => score;

    private int combo = 0;
    private float lastKillTime;
    private Transform comboContainer;

    private void Start()
    {
        ship.OnKill += OnKill;
        ship.OnDeath += ResetCombo;
        counterText.text = "";
        ratingText.text = "";
        comboContainer = ratingText.transform.parent;
    }

    private void OnDestroy()
    {
        if (ship)
        {
            ship.OnKill -= OnKill;
            ship.OnDeath -= ResetCombo;
        }
    }

    private void OnKill()
    {
        lastKillTime = Time.time;
        combo++;

        if (combo <= 1)
        {
            AddScoreForKill();
            return;
        }

        counterText.text = $"{combo}x combo";



        // Check if we have a better rating
        var newRating = ratings
            .TakeWhile((r) => combo >= r.threshold)
            .Count() - 1;
        if (newRating != rating)
        {
            // Display it
            rating = newRating;
            ratingText.text = ratings[rating].name;
            ratingText.color = ratings[rating].color;
            // Set a new rotation so we're looking a little skewed
            var rotation = Quaternion.Euler(0, 0, Random.Range(-20, 20));
            counterText.transform.rotation = rotation;
            ratingText.transform.rotation = rotation;
        }

        AddScoreForKill();

        // Pop in
        comboContainer.transform.localScale = Vector2.one * (1f + (rating + 1) * 0.2f);
        comboContainer.gameObject
            .LeanScale(Vector3.one * (1.2f + (rating + 1) * 0.3f), popInTime)
            .setEasePunch();
    }

    private void AddScoreForKill()
    {
        // TODO Put score on the spot where the enemy was killed
        score += (rating + 2) * 100;
        scoreText.text = $"Score: {score}";
        scoreText.gameObject
            .LeanScale(Vector3.one * 1.3f, popInTime)
            .setEasePunch();
    }

    private void ResetCombo()
    {
        // Reset combo
        combo = 0;
        rating = -1;

        // Pop out
        comboContainer.gameObject.LeanScale(Vector3.zero, popInTime * .6f);
    }

    private void Update()
    {
        if (combo > 0 && Time.time - lastKillTime > comboTimeout)
        {
            ResetCombo();
        }
    }
}
