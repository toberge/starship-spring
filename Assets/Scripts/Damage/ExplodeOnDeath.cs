using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct Drop
{
    public GameObject item;
    public float rarity;
}

public class ExplodeOnDeath : MonoBehaviour
{
    [SerializeField]
    private GameObject explosion;

    [SerializeField]
    private Drop[] drops;

    private void Start()
    {
        GetComponent<Hitbox>().OnDeath += OnDeath;
    }

    private void OnDeath(float damage, float remainingHealth)
    {
        Instantiate(explosion, transform.position, transform.rotation, transform.parent);
        Destroy(gameObject);

        var random = Random.Range(0f, 1f);
        var min = 0f;
        foreach (var drop in drops)
        {
            // Presumes that the rarities sum up to at most 1
            if (random >= min && random <= min + drop.rarity)
            {
                Instantiate(drop.item, transform.position, Quaternion.identity);
                break;
            }
            min += drop.rarity;
        }
    }
}
