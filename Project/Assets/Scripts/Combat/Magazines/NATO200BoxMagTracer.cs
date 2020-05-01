using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NATO200BoxMagTracer : Magazine
{
 
    public NATO200BoxMagTracer ()
    {

        MaxCapacity = 200;
        BulletsInMag = 200;
        BulletWeight = 0.01925f;
        Tracergap = 0;
        BulletDamage = 30;
        NextTracer = 0;
        Name = "7.62×51mm NATO 200 Round Box Mag Tracer";
        CalculateWeight();


    }
}
