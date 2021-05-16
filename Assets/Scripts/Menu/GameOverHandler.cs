using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    public AudioClip OnClickSound;
    public GameObject GoldText;
    public PlayerData Data;
    private AudioSource Audio;

    void Start()
    {
        Audio = this.GetComponent<AudioSource>();

        GoldText.GetComponent<Text>().text = Data.Gold.ToString();
    }

    public void OnMenuClick()
    {
        Audio.PlayOneShot(OnClickSound);
        SceneManager.LoadScene("Menu");
    }
}
