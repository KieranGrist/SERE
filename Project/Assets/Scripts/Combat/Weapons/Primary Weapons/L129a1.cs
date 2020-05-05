using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L129a1 : Weapon
{
    public L129a1()
    {

        Weight = 4.4f;
        FiringRate = .5F;
        FiringSpeed = 1000;
        CompatableMagazines.Add(new NATO20APMag());
        CompatableMagazines.Add(new NATO20HVMag());
        CompatableMagazines.Add(new EmptyMag());
        CurrentMagazine = new EmptyMag();
        Name = "L129A1 Marksman Rifle";

    }
    public override void SwitchFireRate()
    {
        WeaponFireRate++;
        if (WeaponFireRate == RateOfFire.Burst)
            WeaponFireRate = RateOfFire.Saftey;
    }

}
