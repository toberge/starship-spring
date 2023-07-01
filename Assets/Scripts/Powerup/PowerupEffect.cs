using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerupEffect : MonoBehaviour
{
    public abstract void Apply(Ship ship);
    public abstract void Remove(Ship ship);
}
