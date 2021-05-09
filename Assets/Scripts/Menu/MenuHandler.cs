using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public GameObject MainPanel;
    public GameObject OptionPanel;

    public AudioClip OnClickSound;
    public AudioMixer Mixer;

    private Text EffectLabel;
    private Text MusicLabel;

    private AudioSource Audio;

    public void Start()
    {
        EffectLabel = GameObject.Find("Options/EffectText").GetComponent<Text>();
        MusicLabel = GameObject.Find("Options/MusicText").GetComponent<Text>();
        OptionPanel.SetActive(false);
        Audio = this.GetComponent<AudioSource>();
    }

    public void OnOptionsClick()
    {
        MainPanel.SetActive(false);
        OptionPanel.SetActive(true);
        Audio.PlayOneShot(OnClickSound);
    }

    public void OnOptionsBackClick()
    {
        MainPanel.SetActive(true);
        OptionPanel.SetActive(false);
        Audio.PlayOneShot(OnClickSound);
    }

    public void EffectVolumeChange(System.Single value)
    {
        EffectLabel.text = value.ToString() + "%";

        if (value == 0)
        {
            Mixer.SetFloat("EffectVolume", -80);
        } 
        else
        {
            Mixer.SetFloat("EffectVolume", Mathf.Log10(value / 100) * 20);
        }
    }

    public void MusicVolumeChange(System.Single value)
    {
        MusicLabel.text = value.ToString() + "%";

        if (value == 0)
        {
            Mixer.SetFloat("MusicVolume", -80);
        }
        else
        {
            Mixer.SetFloat("MusicVolume", Mathf.Log10(value / 100) * 20);
        }
    }

    public void OnPlayClick()
    {
        Audio.PlayOneShot(OnClickSound);
        SceneManager.LoadScene("Hub");
    }

    public void OnExitClick()
    {
        Audio.PlayOneShot(OnClickSound);
        Application.Quit();
    }
}
