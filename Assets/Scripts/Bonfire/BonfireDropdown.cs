﻿using Assets.Scripts.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BonfireDropdown : MonoBehaviour
{
    public GameObject OptionButton;

    void Start()
    {
        GenerateOptions();
    }

    public void GenerateOptions()
    {
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (BonfireLocation l in BonfireGameState.Locations)
        {
            GameObject go = Instantiate(OptionButton, this.transform);
            var button = go.GetComponent<UnityEngine.UI.Button>();

            if (l.IsLit)
            {
                button.interactable = true;
                button.GetComponentInChildren<Text>().text = l.LocationName;
                button.onClick.AddListener(() => BonfireSelected(l));
            }
            else
            {
                button.interactable = false;
                button.GetComponentInChildren<Text>().text = "??????????";
            }
        }
    }

    public void BonfireSelected(BonfireLocation bonfire)
    {
        Debug.Log("Warping to " + bonfire.LocationName);
        BonfireGameState.BonfireLocation = bonfire.LocationName;
        StartCoroutine(LoadScene(bonfire));
    }

    IEnumerator LoadScene(BonfireLocation bonfire)
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(bonfire.Scene);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {    
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
