using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTarget : Entity
{
    float Gap = 4;
    int Deaths = 0;
    float Timer = 60;
    public List<float> DamageDone = new List<float>();
    public List<float> DamageDoneInAMinute = new List<float>();
    public float TotalDamage;
    public float DamagePerMinute;
    public float AverageDamage;
    public float LastDamage;
    public ShootingTarget()
    {

    }

    public override void DealDamage(float Damage)
    {
        LastDamage = Damage;
        DamageDone.Add(Damage);
        DamageDoneInAMinute.Add(Damage);
        base.DealDamage(Damage);
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        DamagePerMinute = 0;
        foreach(var item in DamageDoneInAMinute)
        {
            DamagePerMinute += item;
        }
        TotalDamage = 0;
        foreach (var item in DamageDone)
        {
            TotalDamage += item;
        }

            Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            Timer = 60;
            DamageDoneInAMinute.Clear();
        }
        if (entityStats.Health > 70)
        {
            Gap = 4;
            GetComponent<Renderer>().material.color = Color.green;
        }
        if (entityStats.Health >40 && entityStats.Health <70)
        {
            GetComponent<Renderer>().material.color = Color.yellow;
        }
        if (entityStats.Health > 0 && entityStats.Health < 40)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        if (entityStats.Health <= 0)
        {
            GetComponent<Renderer>().material.color = Color.black;
            Gap -= Time.deltaTime;
            if (Gap < 0)
            {
                Deaths++;
                entityStats.Health = 100;
            }
        }
            base.Update();
    }
}
