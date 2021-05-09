using Assets.Scripts.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bonfire : MonoBehaviour
{
    int LocationIndex = -1;
    bool IsLit = false;
    GameObject LightEntity = null;
    GameObject UI;

    // Start is called before the first frame update
    void Start()
    {
        LightEntity = this.transform.Find("Lights").gameObject;

        for (int i = 0; i < BonfireGameState.Locations.Count; i++)
        {
            if (BonfireGameState.Locations[i].Scene == SceneManager.GetActiveScene().name)
            {
                if (BonfireGameState.Locations[i].NameInScene == this.gameObject.name)
                {
                    LocationIndex = i;
                    break;
                }
            }
        }

        UI = GameObject.Find("BonfireUI");
        UI.GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        LightEntity.SetActive(BonfireGameState.Locations[LocationIndex].IsLit);
    }

    public void OnInteraction()
    {
        Debug.Log("Interacted with bonfire");

        // On first interaction light the bonfire
        if (!BonfireGameState.Locations[LocationIndex].IsLit)
        {
            BonfireGameState.Locations[LocationIndex].IsLit = true;

            // Regenerate UI
            GameObject go = UI.transform.Find("OptionsPanel/Grid").gameObject;
            go.GetComponent<BonfireDropdown>().GenerateOptions();
        }

        // Show/Hide UI
        if (UI.GetComponent<Canvas>().enabled)
        {
            UI.GetComponent<Canvas>().enabled = false;
        }
        else
        {
            UI.GetComponent<Canvas>().enabled = true;
        }
    }

    public void OnLeave()
    {
        UI.GetComponent<Canvas>().enabled = false;
    }
}
