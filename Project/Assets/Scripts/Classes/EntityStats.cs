using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EntityStats
{
    [Header("Entity Stats")]
    public float Health = 100;
    public float Stamina = 100;
    public float SightRange = 250;
    public float Sensertivity = 1;


    public EntityStats()
    {
        Health = 100;
        Stamina = 100;
        SightRange = 250;
        Sensertivity = 1;
    }



}
