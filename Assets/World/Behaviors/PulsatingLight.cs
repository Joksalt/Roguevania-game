using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PulsatingLight : MonoBehaviour
{
    Light2D lightToManipulate;

    public float Interval = 1.0f;

    [Range(0.0f, 10.0f)]
    public float From = 0.0f;

    [Range(0.0f, 10.0f)]
    public float To = 1.0f;

    bool up = true;

    // Start is called before the first frame update
    void Start()
    {
        lightToManipulate = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (up)
        {
            lightToManipulate.intensity += Interval * Time.deltaTime;

            if (lightToManipulate.intensity >= To)
            {
                up = false;
            }
        } 
        else
        {
            lightToManipulate.intensity -= Interval * Time.deltaTime;

            if (lightToManipulate.intensity <= From)
            {
                up = true;
            }
        }
    }
}
