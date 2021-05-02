using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyController : MonoBehaviour
{
    private HUDController HUDController;

    private PlayerData player;

    public Text MoneyTextValue;

    public void Start()
    {
        HUDController = this.transform.parent.gameObject.GetComponent<HUDController>();
        player = HUDController.Player.GetComponent<Player>().playerData;
    }

    public void Update()
    {
        //Debug.Log(player.CurrentHealth);
        MoneyTextValue.text = $"{player.Gold}";
    }
}
