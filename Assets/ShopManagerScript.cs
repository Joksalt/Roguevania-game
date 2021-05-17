﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManagerScript : MonoBehaviour
{
    public int[,] shopItems = new int[5, 5];
    public Text coinsTxt;
    public Player player;
    private PlayerData playerData;


    // Start is called before the first frame update
    void Start()
    {
        playerData = player.GetComponent<Player>().playerData;
        //coinsTxt.text = "Coins:" + coins.ToString();
        coinsTxt.text = "Coins:" + playerData.Gold;

        //item IDs
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        shopItems[1, 4] = 4;


        //Price 
        shopItems[2, 1] = 10;
        shopItems[2, 2] = 20;
        shopItems[2, 3] = 30;
        shopItems[2, 4] = 40;

        //Quantity
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;
        shopItems[3, 4] = 0;

    }

    // Update is called once per frame
    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if (playerData.Gold >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID])
        {
            playerData.Gold -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID];
            shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID]++;
            coinsTxt.text = "Coins:" + playerData.Gold;
            ButtonRef.GetComponent<ButtonInfo>().QuantityTxt.text = shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID].ToString();
        }
        else
        {
            coinsTxt.text = "Insufficient funds!";
        }
    }
}