using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ShootingTarget : Player
{
  public  NavMeshAgent AINavMeshAgent;
    float Gap = 4;
   public int Deaths = 0;
    float Timer = 60;
    public bool InfiniteHealth;
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

    new void  Start()
    {
        base.Start();
        AINavMeshAgent = GetComponent<NavMeshAgent>();
        Control = false;
        Affiliation = Side.Enemy;
    }

    public override void Update()
    {
        if (Affiliation == Side.Enemy)
            AINavMeshAgent.destination = GM.ExtractionGameObject.transform.position;
        else
            AINavMeshAgent.destination = transform.position;
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
            Affiliation = Side.Enemy;
            Gap = 4;
            GetComponent<Renderer>().material.color = Color.green;
        }
        if (entityStats.Health >40 && entityStats.Health <70)
        {
            Affiliation = Side.Enemy;
            GetComponent<Renderer>().material.color = Color.yellow;
        }
        if (entityStats.Health > 0 && entityStats.Health < 40)
        {
            Affiliation = Side.Enemy;
            GetComponent<Renderer>().material.color = Color.red;
        }
        if (entityStats.Health <= 0 && InfiniteHealth)
        {
            GetComponent<Renderer>().material.color = Color.black;
            Gap -= Time.deltaTime;
            if (Gap < 0)
            {
                Deaths++;
                entityStats.Health = 100;
            }
        }
        if (entityStats.Health <= 0 && !InfiniteHealth)
        {
            Affiliation = Side.Civilian;
        }
            base.Update();
    }

    public override void Restart()
    {
        base.Restart();
        AINavMeshAgent = GetComponent<NavMeshAgent>();
        Control = false;
        entityStats = new EntityStats();
        Affiliation = Side.Enemy;

    }
}
