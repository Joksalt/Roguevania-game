using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BossHPController : MonoBehaviour
{
    public Text HPTextValue;
    private Slider slider;
    private GameObject Boss;
    private float currBossHealth;
    private float maxBossHealth;
    void Start()
    {
        slider = GetComponent<Slider>();
        Boss = GameObject.FindGameObjectWithTag("Boss");
        currBossHealth = maxBossHealth = Boss.GetComponent<Mobs>().maxHealth;

        slider.maxValue = currBossHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currBossHealth = slider.value = Boss.GetComponent<Mobs>().currentHealth;
        HPTextValue.text = $"{currBossHealth} / {maxBossHealth}";

        if (currBossHealth <= 0)
        {
            // Game over
            HPTextValue.text = "0";
        }

    }
}
