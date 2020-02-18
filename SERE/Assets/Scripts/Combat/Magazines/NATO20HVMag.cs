using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NATO20HVMag : Magazine
{
    public NATO20HVMag()
    {
        MaxCapacity = 20;
        BulletsInMag = 20;
        BulletWeight = 0.124f;
        Tracergap = 2;
        BulletDamage = 70;
        NextTracer = 2;
        Name = "7.62×51mm NATO 20 Round High Velocity Magazine";
        CalculateWeight();
    }
}
