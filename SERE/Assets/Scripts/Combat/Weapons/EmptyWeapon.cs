using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyWeapon : Weapon
{
    public EmptyWeapon()
    {

        Weight = 0;
        FiringRate = 0;
        FiringSpeed = 0;
        CompatableMagazines.Add(new EmptyMag());
        CurrentMagazine = new EmptyMag();
        Name = "No Weapon";

    }
}
