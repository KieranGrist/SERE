using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NATO30StandardMag : Magazine
{
    public NATO30StandardMag()
    {
        MaxCapacity = 30;
        BulletsInMag = 30;
        BulletWeight = 0.062f;
        Tracergap = 3;
        BulletDamage = 40;
        NextTracer = 3;
        Name = "5.5.56×45mm NATO 30 Round Standard Magazine";
        CalculateWeight();
    }
}
