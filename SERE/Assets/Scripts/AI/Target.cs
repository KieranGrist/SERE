using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Timers;
public class Target : Entity
{
    public List<float> DPSDamageRecieved;
    public List<float> DamageRecieved;
    public float[] LastThreeDamage = new float [3];
    public float TotalDamage;
    public float DamagePerSecond;
    public float HighestDPS;
    public Text text;
    

    public override void DealDamage(float Damage)
    {
        DamagePerSecond += Damage;
        DPSDamageRecieved.Add(Damage);
        DamageRecieved.Add(Damage);
        LastThreeDamage[2] = LastThreeDamage[1];
        LastThreeDamage[1] = LastThreeDamage[0];
        LastThreeDamage[0] = DamagePerSecond;

    }
    public override void Start()
    {
        base.Start();
        InvokeRepeating("OnTimedEvent", 0, 1.0f);
    }
    private  void OnTimedEvent()
    {
        DamagePerSecond = 0;
        foreach (var item in DPSDamageRecieved)
        {
            DamagePerSecond += item;
        }
        if (DamagePerSecond > HighestDPS)
            HighestDPS = DamagePerSecond;
        DPSDamageRecieved.Clear();
    }
    void UpdateText()
    {
        text.text = "Damage Per Second: " + DamagePerSecond + "\n" + LastThreeDamage[0] + "\n" + LastThreeDamage[1] + "\n" + LastThreeDamage[2]; 
    }
    public override void Update()
    {
        base.Update();

        UpdateText();
    
        TotalDamage = 0;
        foreach(var item in DamageRecieved)
        {
            TotalDamage += item;
        }

    }
}

