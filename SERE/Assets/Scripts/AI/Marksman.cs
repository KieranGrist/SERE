using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marksman : Soldier
{
   public Marksman()
    {
        PrimaryWeapon = new L129a1();
        SecondaryWeapon = new Glock17();
        TertiaryWeapon = new L85A2();
        CurrentWeapon = PrimaryWeapon;
        inventory = new Inventory();
        inventory.AddItem(CurrentWeapon);
        inventory.AddItem(SecondaryWeapon);
        inventory.AddItem(TertiaryWeapon);

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
        VisionDistance = 1000;
        CombatDistance = 500;
    }
}
