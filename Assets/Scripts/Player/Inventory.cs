using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject[] inventory = new GameObject[10];
    public Button[] inventoryButtons = new Button[10];

    public void AddItem(GameObject item)
    {
        bool itemAdded = false;

        // Find first open slot in inventory
        for (int i = 0; i < inventory.Length; i++)
        {
            if(inventory[i] == null)
            {                
                //Adding item to inventory
                inventory[i] = item;
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
        for (int i = 0; i < inventory.Length; i++)
        {
            if(inventory[i] == item)
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
        for (int i = 0; i < inventory.Length; i++)
        {
            if(inventory[i] != null)
            {
                if(inventory[i].GetComponent<InteractionObject>().itemType == itemType)
                {
                    // We found the item of the type we are looking for 
                    return inventory[i];
                }
            }
        }

        // Item of type not found 
        return null;
    }

    public void RemoveItem(GameObject item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if(inventory[i]== item)
            {
                // We found the item - remove it 
                inventory[i] = null;
                Debug.Log($"Item {item.name} was removed from inventory!");

                // Update UI
                inventoryButtons[i].image.overrideSprite = null;
                break;
            }
        }
    }
}
