using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    //If true, object can be stored in inventory
    public bool inventory;
    public bool shop;

    public void DoInteraction()
    {
        //Interact with shop NPC
        if(gameObject.name == "NPC")
        {
            GameObject shop = gameObject.transform.Find("Shop_UI").gameObject;
            if (shop.activeSelf)
                shop.SetActive(false);
            else
                shop.SetActive(true);
        }

        if(gameObject.name == "iron")
        {
            gameObject.SetActive(false);
        }

        //Turn on Shop UI
        //GameObject shop = GameObject.Find("Shop_UI");
        //shop.SetActive(false);
    }
}
