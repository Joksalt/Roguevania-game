using Assets.Scripts.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class StateInitializer : MonoBehaviour
{
    public BonfireData InitialData;

    public AudioMixerGroup EffectGroup;
    public AudioMixerGroup MusicGroup;

    void Start()
    {
        // Load if needed by default deep copy initial data
        if (BonfireGameState.Locations.Count == 0)
        {
            for (int i = 0; i < InitialData.Locations.Length; i++)
            {
                BonfireGameState.Locations.Add(InitialData.Locations[i].Clone());
            }
        }

        AudioState.MusicGroup = MusicGroup;
        AudioState.EffectGroup = EffectGroup;
    }
}
