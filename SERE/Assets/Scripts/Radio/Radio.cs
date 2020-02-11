using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Radio", menuName = "InventoryItems/Radio", order = 1)]
public  class Radio : InventoryItem
{
    [Header("Radio")]
    public int Frequency;
    public float RadioTransmistionDistance;

    public void HearMessage()
    {

    }

    public void Transmit()
    {

    }
}
