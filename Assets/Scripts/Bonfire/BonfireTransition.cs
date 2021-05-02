using Assets.Scripts.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BonfireTransition : MonoBehaviour
{
    void Start()
    {
        if (BonfireGameState.BonfireLocation != "")
        {
            // Find the bonfire game object
            for(int i = 0; i < BonfireGameState.Locations.Count; i++)
            {
                if (BonfireGameState.Locations[i].LocationName == BonfireGameState.BonfireLocation)
                {
                    Debug.Log("Positioning at " + BonfireGameState.BonfireLocation);
                    GameObject go = GameObject.Find(BonfireGameState.Locations[i].NameInScene);
                    this.transform.position = go.transform.position + new Vector3(0, 2, 0);
                    return;
                }
            }
        }
    }
}
