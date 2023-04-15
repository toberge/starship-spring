using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    private float fireRate = .5f;

    private FirePattern pattern;

    private float lastFireTime = 0;

    private void Start()
    {
        pattern = GetComponent<FirePattern>();
    }

    void Update()
    {
        Debug.Log(Time.time - lastFireTime);
        if (Time.time - lastFireTime >= fireRate)
        {
            pattern.Fire(Time.time);
            lastFireTime = Time.time;
        }
    }
}
