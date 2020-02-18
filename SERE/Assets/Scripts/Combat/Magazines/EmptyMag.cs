using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyMag : Magazine
{
   public EmptyMag()
    {
        MaxCapacity = 0;
        BulletsInMag = 0;
        BulletWeight = 0;
        Tracergap = 0;
        BulletDamage = 0;
        NextTracer = 0;
        Name = "No Magazine";
        CalculateWeight();
    }
}
