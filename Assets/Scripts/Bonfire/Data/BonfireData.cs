using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BonfireLocation
{
    public string LocationName;
    public string Scene;
    public string NameInScene;
    public bool IsLit;

    public BonfireLocation Clone()
    {
        BonfireLocation bl = new BonfireLocation();
        bl.LocationName = LocationName;
        bl.Scene = Scene;
        bl.NameInScene = NameInScene;
        bl.IsLit = IsLit;
        return bl;
    }
}

[CreateAssetMenu(fileName = "newBonfireData", menuName = "Data/Bonfire Data/Base Data")]
public class BonfireData : ScriptableObject
{
    [Header("Mapping")]
    public BonfireLocation[] Locations;
}