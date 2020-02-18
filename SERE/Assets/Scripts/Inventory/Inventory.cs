using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Inventory { 
    public List<InventoryItem> inventoryItems = new List<InventoryItem>();
    public float MaxiumWeight = 50.0f;
    public float Weight;
    public Inventory()
    {
        CalculateWeight();
    }
    public void AddItem(InventoryItem item)
    {
        inventoryItems.Add(item);
    }
    public void RemoveItem(InventoryItem item)
    {
        inventoryItems.Remove(item);
    }   
    public InventoryItem SearchAndReturn (InventoryItem SearchItem)
    {
        foreach(var item in inventoryItems)
        {
            if (item.Name == SearchItem.Name)
            {
                RemoveItem(item);
                return item;              
            }
        }
        return new InventoryItem();
    }
    public void CalculateWeight()
    {
        Weight = 0;
        foreach(var item in inventoryItems)
        {
            Weight += item.Weight;
        }
    }
}

