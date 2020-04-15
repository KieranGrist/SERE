using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EntityStats
{
    [Header("Entity Stats")]
    public float Health = 100;
    public float Stamina = 100;
    public float SightRange = 50;
    public float SmellRange = 50;
    public float EarRange = 50;
    public float Mass = 1;
    public float Sensertivity = 1;
    public float DefaultSpeed = 5;
    public float CurrentSpeed = 5;


    public EntityStats()
    {
        Health = 50;
        Stamina = 100;
        SightRange = 50;
        SmellRange = 50;
        EarRange = 50;
        Mass = 1;
        Sensertivity = 1;
        DefaultSpeed = 5;
        CurrentSpeed = 5;
    }

    public EntityStats(
        float Health = 100,
     float Stamina = 100,
     float SightRange = 50,
     float SmellRange = 50,
     float EarRange = 50,
     float Mass = 1,
     float Sensertivity = 1,
     float DefaultSpeed = 5,
     float CurrentSpeed = 5)
    {
        this.Health = Health;
        this.Stamina = Stamina;
        this.SightRange = SightRange;
        this.SmellRange = SmellRange;
        this.EarRange = EarRange;
        this.Mass = Mass;
        this.Sensertivity = Sensertivity;
        this.DefaultSpeed = DefaultSpeed;
        this.CurrentSpeed = CurrentSpeed;
    }


}
