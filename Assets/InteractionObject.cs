using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    // If true, object can be stored in inventory
    public bool inventory;
    // If true, object diplays its goods
    public bool shop;
    // If true, object can talk
    public bool talks;
    // If true, item gives power
    public bool powerup;
    // Objects message if it talks
    public string message;
    // This will tell what type of item this object is 
    public string itemType;

    public void DoInteraction()
    {
        gameObject.SetActive(false);
    }

    public void OpenShop()
    {
        // Interact with shop NPC
        if (gameObject.name == "NPC")
        {
            GameObject shop = gameObject.transform.Find("Shop_UI").gameObject;
            if (shop.activeSelf)
                shop.SetActive(false);
            else
            {
                Debug.Log(message);
                shop.SetActive(true);
            }
                
        }
    }

    public void Talk()
    {
        Debug.Log(message);
    }
}
