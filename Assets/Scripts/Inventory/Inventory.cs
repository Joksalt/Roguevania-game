using Assets.Scripts.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Button[] inventoryButtons = new Button[10];

    void Start()
    {
        // Find first open slot in inventory
        for (int i = 0; i < InventoryState.Inventory.Length; i++)
        {
            if (InventoryState.Inventory[i] != null)
            {
                //Updating inventory's UI 
                inventoryButtons[i].image.overrideSprite = InventoryState.Inventory[i].GetComponent<SpriteRenderer>().sprite;
            }
        }
    }

    public void AddItem(GameObject item)
    {
        bool itemAdded = false;

        // Find first open slot in inventory
        for (int i = 0; i < InventoryState.Inventory.Length; i++)
        {
            if(InventoryState.Inventory[i] == null)
            {
                //Adding item to inventory
                InventoryState.Inventory[i] = item;
                DontDestroyOnLoad(InventoryState.Inventory[i]);
                //Updating inventory's UI 
                inventoryButtons[i].image.overrideSprite = item.GetComponent<SpriteRenderer>().sprite;
                itemAdded = true;
                item.SendMessage("DoInteraction");
                Debug.Log(item.name + " was added");
                break;
            }
        }

        // Inventory was full
        if (!itemAdded)
        {
            Debug.Log("Inventory is full - item not added");
        }
    }

    public bool FindItem(GameObject item)
    {
        for (int i = 0; i < InventoryState.Inventory.Length; i++)
        {
            if(InventoryState.Inventory[i] == item)
            {
                // Item found 
                return true;
            }
        }

        // Item not found
        return false;
    }

    public GameObject FindItemByType(string itemType)
    {
        for (int i = 0; i < InventoryState.Inventory.Length; i++)
        {
            if(InventoryState.Inventory[i] != null)
            {
                if(InventoryState.Inventory[i].GetComponent<InteractionObject>().itemType == itemType)
                {
                    // We found the item of the type we are looking for 
                    return InventoryState.Inventory[i];
                }
            }
        }

        // Item of type not found 
        return null;
    }

    public void RemoveItem(GameObject item)
    {
        for (int i = 0; i < InventoryState.Inventory.Length; i++)
        {
            if(InventoryState.Inventory[i]== item)
            {
                // We found the item - remove it 
                InventoryState.Inventory[i] = null;
                Debug.Log($"Item {item.name} was removed from inventory!");

                // Update UI
                inventoryButtons[i].image.overrideSprite = null;
                break;
            }
        }
    }

    public bool IsFull()
    {
        for(int i = 0; i < InventoryState.Inventory.Length; i++)
        {
            if (InventoryState.Inventory[i] == null)
            {
                return false;
            }

        }
            
        return true;
    }
}
