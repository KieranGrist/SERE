using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NATO20APMag : Magazine
{
    public NATO20APMag()
    {
        MaxCapacity = 20;
        BulletsInMag = 20;
        BulletWeight = 0.144f;
        Tracergap = 2;
        BulletDamage = 60;
        NextTracer = 2;
        Name = "7.62×51mm NATO 20 Round Armour Piercieing Magazine";
        CalculateWeight();
    }
}
