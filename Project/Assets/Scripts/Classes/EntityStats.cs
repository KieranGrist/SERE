using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EntityStats
{
    [Header("Entity Stats")]
    public float Health = 100;
    public float Stamina = 100;
    public float sightRange = 15;
    public EntityStats()
    {
        Health = 100;
        Stamina = 100;
    }

    public float SightRange { get => sightRange;}
}
