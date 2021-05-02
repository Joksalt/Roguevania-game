using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTracking : MonoBehaviour
{
    public GameObject Target;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - Target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Target.transform.position + offset;
    }
}
