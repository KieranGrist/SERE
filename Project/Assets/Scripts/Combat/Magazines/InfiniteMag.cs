using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteMagazine : Magazine
{
   public InfiniteMagazine()
    {
        MaxCapacity = 9999999;
        BulletsInMag = 999999;
        BulletWeight = 0;
        Tracergap = 0;
        BulletDamage = 0;
        NextTracer = 0;
        Name = "Infinite Magazine";
        CalculateWeight();
    }
}
