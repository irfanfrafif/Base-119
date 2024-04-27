using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    private Dictionary<int, int> inventory = new Dictionary<int, int>();

    public void AddItem(int itemID)
    {
        AddItem(itemID, 1);
    }
    public void AddItem(int itemID, int amount)
    {
        if(inventory.ContainsKey(itemID))
        {
            inventory[itemID] += amount;
        }
        else
        {
            inventory.Add(itemID, 1);
        }
    }

    public void RemoveItem(int itemID)
    {
        RemoveItem(itemID, 1);
    }
    public void RemoveItem(int itemID, int amount)
    {
        if (inventory.ContainsKey(itemID))
        {
            inventory[itemID] -= amount;

            if (inventory[itemID] <= 0)
            {
                inventory.Remove(itemID);
            }
        }
        else
        {
            Debug.Log("Item doesn't exist");
        }
    }

    public int GetAmount(int itemID)
    {
        if (!inventory.ContainsKey(itemID)) return 0;

        return inventory[itemID];
    }

    public bool ContainsItem(int itemID)
    {
        return inventory.ContainsKey(itemID);
    }
}
