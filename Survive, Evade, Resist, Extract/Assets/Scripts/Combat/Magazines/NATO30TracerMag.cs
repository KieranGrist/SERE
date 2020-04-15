using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NATO30TracerMag : Magazine
{
    public NATO30TracerMag()
    {
        MaxCapacity = 30;
        BulletsInMag = 30;
        BulletWeight = 0.062f;
        Tracergap = 0;
        BulletDamage = 40;
        NextTracer = 0;
        Name = "5.5.56×45mm NATO 30 Round Tracer Magazine";
        CalculateWeight();
    }
    // Start is called before the first frame update
public    void Start()
    {
        
    }


}
