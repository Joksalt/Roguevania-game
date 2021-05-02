using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTracking : MonoBehaviour
{
    public GameObject Target;
    public Vector3 offset;

    void Update()
    {
        transform.position = Target.transform.position + offset;
    }
}
