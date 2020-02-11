using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Magazine", menuName = "InventoryItems/Magazine", order = 1)]
[System.Serializable]   
public class Magazine : InventoryItem
{
    [Header("Magazine")]
    public  float MaxCapacity;
    public  float BulletsInMag;
    public  float BulletWeight;
    public  float Tracergap = 3000;
    public   float BulletDamage;
    public   float NextTracer;

    [Header("Run Time ")]
    public float RunTimeMaxCapacity;
    public float RunTimeBulletsInMag;
    public float RunTimeBulletWeight;
    public float RunTimeTracergap = 3000;
    public float RunTimeBulletDamage;
    public float RunTimeNextTracer;

    public void Start()
    {
         RunTimeMaxCapacity = MaxCapacity;
     RunTimeBulletsInMag = BulletsInMag;
RunTimeBulletWeight = BulletWeight;
        RunTimeTracergap = Tracergap; 
   RunTimeBulletDamage = BulletDamage;
        RunTimeNextTracer = NextTracer;
}
    public  void CalculateWeight()
    {
      RunTimeWeight = BulletWeight * BulletsInMag;
    }
}
