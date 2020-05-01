using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NATO200Boxmag : Magazine
{
    public NATO200Boxmag()
    {
        MaxCapacity = 100;
        BulletsInMag = 00;
        BulletWeight = 0.01925f;
        Tracergap = 4;
        BulletDamage = 30;
        NextTracer = 0;
        Name = "7.62×51mm NATO 200 Round Box Mag";
        CalculateWeight();
    }
}
