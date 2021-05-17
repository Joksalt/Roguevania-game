using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public UnityEvent IntercationEvent;
    public UnityEvent LeaveEvent;

    public void DoInteraction()
    {
        gameObject.SetActive(false);
    }

    public void OpenShop()
    {
        // Interact with shop NPC
        if (gameObject.name == "NPC")
        {
            //GameObject shop = gameObject.transform.Find("Shop_UI").gameObject;
            GameObject shopUi = gameObject.transform.Find("Shop_UI2").gameObject;
            if (/*shop.activeSelf || */shopUi.activeSelf)
            {
                //shop.SetActive(false);
                shopUi.SetActive(false);

            }                
            else
            {
                Debug.Log(message);
                //shop.SetActive(true);
                shopUi.SetActive(true);
            }
                
        }
    }

    public void GenericInteraction()
    {
        if (IntercationEvent != null)
        {
            IntercationEvent.Invoke();
        }
    }

    public void GenericLeave()
    {
        if (LeaveEvent != null)
        {
            LeaveEvent.Invoke();
        }
    }

    public void Talk()
    {
        Debug.Log(message);
    }
}
