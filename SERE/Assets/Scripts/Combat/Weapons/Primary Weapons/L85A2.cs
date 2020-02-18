using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L85A2 : Weapon
{
  public L85A2()
    {
        Weight = 4.98f;
        FiringRate = .25F;
        FiringSpeed = 10000;
        CompatableMagazines.Add(new NATO30TracerMag());
        CompatableMagazines.Add(new NATO30StandardMag());
        CompatableMagazines.Add(new NATO30NoTracerMag());
        CompatableMagazines.Add(new EmptyMag());
        CurrentMagazine = new EmptyMag();
        Name = "L85A2 Rifle";
    }
    public override void SwitchFireRate()
    {
        WeaponFireRate++;
        if (WeaponFireRate == RateOfFire.Burst)
            WeaponFireRate++;
        if (WeaponFireRate == RateOfFire.Max)
            WeaponFireRate = RateOfFire.Saftey;
    }

}
