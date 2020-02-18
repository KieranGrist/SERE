using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glock17 : Weapon
{
    public Glock17()
    {
        Weight = 0.01768131408f;
        FiringRate = .5F;
        CompatableMagazines.Add(new NATO17TracerMag());
        CompatableMagazines.Add(new NATO17StandardMag());
        CompatableMagazines.Add(new EmptyMag());
        CurrentMagazine = new EmptyMag();
        Name = "Glock 17 Pistol";
    }
}
