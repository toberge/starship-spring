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

        foreach (var drop in drops)
        {
            // TODO proper weighted draw
            if (Random.Range(0f, 1f) < 0.1)
            {
                Instantiate(drop.item, transform.position, Quaternion.identity);
            }
        }
    }
}
