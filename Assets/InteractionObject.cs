using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{

    public void DoInteraction()
    {
        //Picked up and put in inventory
        if(gameObject.name == "NPC")
        {
            GameObject shop = gameObject.transform.Find("Shop_UI").gameObject;
            if (shop.activeSelf)
                shop.SetActive(false);
            else
                shop.SetActive(true);
        }
        //gameObject.SetActive(false);

        //Turn on Shop UI
        //GameObject shop = GameObject.Find("Shop_UI");
        //shop.SetActive(false);
    }
}
