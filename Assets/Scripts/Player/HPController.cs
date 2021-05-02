using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    private HUDController HUDController;

    private Slider slider;
    private PlayerData player;

    public Text HPTextValue;

    public void Start()
    {
        slider = GetComponent<Slider>();
        HUDController = this.transform.parent.gameObject.GetComponent<HUDController>();
        player = HUDController.Player.GetComponent<Player>().playerData;

        slider.maxValue = player.MaxHealth;
    }

    public void Update()
    {
        //Debug.Log(player.CurrentHealth);
        slider.value = player.CurrentHealth;
        HPTextValue.text = $"{player.CurrentHealth} / {player.MaxHealth}";
    }
}
