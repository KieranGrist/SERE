using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class MachineGunner : Soldier
{
    public MachineGunner()
    {
    
        combat.PrimaryWeapon = new L7A2();
        combat.SecondaryWeapon = new Glock17();
        inventory = new Inventory();
        inventory.AddItem(combat.PrimaryWeapon);
        inventory.AddItem(combat.SecondaryWeapon);
        for (int i = 0; i < 4; i++)
            inventory.AddItem(new NATO200BoxMagTracer());
        for (int i = 0; i < 2; i++)
        {
            inventory.AddItem(new NATO17StandardMag());
            inventory.AddItem(new NATO17TracerMag());
        }
        combat.CurrentWeapon = combat.PrimaryWeapon;
        inventory.CalculateWeight();
    }
    public override void Restart()
    {
        base.Restart();
        combat.PrimaryWeapon = new L7A2();
        combat.SecondaryWeapon = new Glock17();
        combat.CurrentWeapon = combat.PrimaryWeapon;
        inventory = new Inventory();
        inventory.AddItem(combat.PrimaryWeapon);
        inventory.AddItem(combat.SecondaryWeapon);
        for (int i = 0; i < 4; i++)
            inventory.AddItem(new NATO200BoxMagTracer());
        for (int i = 0; i < 2; i++)
        {
            inventory.AddItem(new NATO17StandardMag());
            inventory.AddItem(new NATO17TracerMag());
        }
        inventory.CalculateWeight(); 
    }
    public override void Start()
    {
        Restart();
    }
}
