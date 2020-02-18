using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NATO30NoTracerMag : Magazine
{
    public NATO30NoTracerMag()
    {
        MaxCapacity = 30;
        BulletsInMag = 30;
        BulletWeight = 0.062f;
        Tracergap = 3;
        BulletDamage = 40;
        NextTracer = 3;
        Name = "5.5.56×45mm NATO 30 Round No Tracer Magazine";
        CalculateWeight();
    }
}
