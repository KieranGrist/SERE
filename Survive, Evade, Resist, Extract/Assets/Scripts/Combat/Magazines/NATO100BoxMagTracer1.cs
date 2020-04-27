using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NATO100BoxMagTracer : Magazine
{
    public NATO100BoxMagTracer()
    {

        MaxCapacity = 100;
        BulletsInMag = 00;
        BulletWeight = 0.01925f;
        Tracergap = 0;
        BulletDamage = 30;
        NextTracer = 0;
        Name = "7.62×51mm NATO 100 Round Box Mag Tracer";
        CalculateWeight();
    }
}
