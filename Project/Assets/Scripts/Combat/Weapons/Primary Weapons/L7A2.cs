using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L7A2 : Weapon
{
    public L7A2()
    {
        Weight = 10.99f;
        FiringRate = .15f;
        FiringSpeed = 10000;
        CompatableMagazines.Add(new NATO100BoxMag());
        CompatableMagazines.Add(new NATO200Boxmag());
        CompatableMagazines.Add(new NATO100BoxMagTracer());
        CompatableMagazines.Add(new NATO200BoxMagTracer());
        CurrentMagazine = new EmptyMag();
        Name = "L7A2 GPMG";
    }

    public override void SwitchFireRate()
    {
        if (WeaponFireRate == RateOfFire.Saftey)
        {
            WeaponFireRate = RateOfFire.Automatic;
            return;
        }
        if (WeaponFireRate == RateOfFire.Automatic)
            WeaponFireRate = RateOfFire.Saftey;
    }
}
