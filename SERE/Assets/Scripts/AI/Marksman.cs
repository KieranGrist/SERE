using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marksman : Soldier
{
   public Marksman()
    {
        combat.PrimaryWeapon = new L129a1();
        combat.SecondaryWeapon = new Glock17();
        combat.TertiaryWeapon = new L85A2();
        combat.CurrentWeapon = combat.PrimaryWeapon;
        inventory = new Inventory();
        inventory.AddItem(combat.CurrentWeapon);
        inventory.AddItem(combat.SecondaryWeapon);
        inventory.AddItem(combat.TertiaryWeapon);

        ANPRC152 aNPRC152 = new ANPRC152();
        AIRadio = aNPRC152;
        inventory.AddItem(aNPRC152);
        for (int i = 0; i < 40; i++)
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
        brain.VisionDistance = 1000;
       brain. CombatDistance = 500;
    }
    public override void Restart()
    {
        combat.PrimaryWeapon = new L129a1();
        combat.SecondaryWeapon = new Glock17();
        combat.TertiaryWeapon = new L85A2();
        base.Restart();
        combat.CurrentWeapon = combat.PrimaryWeapon;
        inventory = new Inventory();
        inventory.AddItem(combat.CurrentWeapon);
        inventory.AddItem(combat.SecondaryWeapon);
        inventory.AddItem(combat.TertiaryWeapon);

        ANPRC152 aNPRC152 = new ANPRC152();
        AIRadio = aNPRC152;
        inventory.AddItem(aNPRC152);
        for (int i = 0; i < 40; i++)
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
        brain.VisionDistance = 1000;
        brain.CombatDistance = 500;

    }
}
