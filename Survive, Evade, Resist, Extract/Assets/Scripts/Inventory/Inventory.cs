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
    public bool ItemInInventory(string SearchItem)
    {
        foreach (var item in inventoryItems)
        {
            if (item.Name == SearchItem)
            {
                return true;
            }
        }
        return false;
    }
        public bool ItemInInventory(InventoryItem SearchItem)
    {
        foreach (var item in inventoryItems)
        {
            if (item.Name == SearchItem.Name)
            {
                return true;
            }
        }
        return false;
    }
    public InventoryItem SearchAndCopy(InventoryItem SearchItem)
    {
        foreach (var item in inventoryItems)
        {
            if (item.Name == SearchItem.Name)
            {      
                return item;
            }
        }
        return null;
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
        return null;
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

