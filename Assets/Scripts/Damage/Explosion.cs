using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private float timeToLive = 5;

    void Start()
    {
        Destroy(gameObject, timeToLive);
    }
}
