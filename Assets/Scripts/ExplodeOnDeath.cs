using UnityEngine;

public class ExplodeOnDeath : MonoBehaviour
{
    [SerializeField]
    private GameObject explosion;

    private void Start()
    {
        GetComponent<Hitbox>().OnDeath += OnDeath;
    }

    private void OnDeath(float damage, float remainingHealth)
    {
        Instantiate(explosion, transform.position, transform.rotation, transform.parent);
        Destroy(gameObject);
    }
}
