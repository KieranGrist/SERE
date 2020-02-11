using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryItem : ScriptableObject
{
    [Header("Item")]
    public readonly float Weight;

    [Header("Run Time ")]
    public float RunTimeWeight;

}
