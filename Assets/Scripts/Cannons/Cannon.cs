using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    private float fireRate = .5f;

    private Pattern pattern;

    private float lastFireTime = 0;

    private void Start()
    {
        pattern = GetComponent<Pattern>();
    }

    void Update()
    {
        if (Time.time - lastFireTime >= fireRate)
        {
            pattern.Fire(Time.time);
            lastFireTime = Time.time;
        }
    }
}
