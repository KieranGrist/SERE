using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    int scentSphere = 0;
    public Player()
    {
        Restart();
    }
    public override void Start()
    {
        Restart();
       combat.CurrentWeapon.LoadPrefabs();
        InvokeRepeating("CreateScenetTrail", 15, 15);
    }
    public override void Restart()
    {
        base.Restart();
        Control = true;
        Affiliation = Side.Enemy;
        combat.PrimaryWeapon = new L85A2();
        combat.SecondaryWeapon = new Glock17();
        combat.TertiaryWeapon = new L129a1();
        Affiliation = Side.Enemy;
        combat.CurrentWeapon = combat.PrimaryWeapon;
        inventory = new Inventory();
        inventory.AddItem(combat.CurrentWeapon);
        inventory.AddItem(combat.SecondaryWeapon);
        inventory.AddItem(combat.TertiaryWeapon);

        for (int i = 0; i < 4; i++)
            inventory.AddItem(new NATO20APMag());
        for (int i = 0; i < 6; i++)
            inventory.AddItem(new NATO20HVMag());
        for (int i = 0; i < 4; i++)
            inventory.AddItem(new NATO30TracerMag());
        for (int i = 0; i < 2; i++)
        {
            inventory.AddItem(new NATO17StandardMag());
            inventory.AddItem(new NATO17TracerMag());
        }
        inventory.CalculateWeight();
        combat.CurrentWeapon.LoadPrefabs();
        inventory.CalculateWeight();
    }
    public override void Update()
    {
        base.Update();


    }
    void CreateScenetTrail()
    {
        scentSphere++;

        GameObject st = Resources.Load("ScentSphere") as GameObject;
        GameObject go = Instantiate(st);
        var scent = go.GetComponent<ScentSphere>();
        go.name = "Scent Sphere : " + scentSphere;
        go.transform.position = transform.position;
        scent.TravelingDirection = transform.forward;
        scent.TravelingLocation = transform.position;
    }    
}