﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]   
public class Magazine : InventoryItem
{  
   [Header("Magazine Settings")]
    public   float MaxCapacity;
    public  float BulletsInMag;
    public  float BulletWeight;
   public    float Tracergap = 3000;
    public float BulletDamage;
    public   float NextTracer;

    public  void CalculateWeight()
    {
      Weight = BulletWeight * BulletsInMag;
    }
}
