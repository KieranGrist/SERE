using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NATO17TracerMag : Magazine
{
  public NATO17TracerMag()
    {

        MaxCapacity = 17;
        BulletsInMag = 17;
        BulletWeight = 0.125f;
        Tracergap = 0;
        BulletDamage = 20;
        NextTracer = 0;
        Name = "9x19 mm NATO 17 Round Tracer Magazine";
        CalculateWeight();
    }
}
