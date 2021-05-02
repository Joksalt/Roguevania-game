using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    // Entities that are affected
    public Transform[] Backgrounds;

    // Entity Z scale values
    private float[] BGScales;

    // Parallax movement speed
    public float Smooth = 1.0f;

    // MainCamera transform and position
    private Transform camTransform;
    private Vector3 prevCamPosition;

    // Awake is called before Start()
    void Awake()
    {
        camTransform = Camera.main.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        prevCamPosition = camTransform.position;
        BGScales = new float[Backgrounds.Length];

        for (int i = 0; i < Backgrounds.Length; i++)
        {
            BGScales[i] = Backgrounds[i].position.z;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Backgrounds.Length; i++)
        {
            float parallax = (prevCamPosition.x - camTransform.position.x) * BGScales[i];
            float bgTargetPosX = Backgrounds[i].position.x + parallax;
            Vector3 bgTargetPos = new Vector3(bgTargetPosX, Backgrounds[i].position.y, Backgrounds[i].position.z);
            Backgrounds[i].position = Vector3.Lerp(Backgrounds[i].position, bgTargetPos, Smooth * Time.deltaTime);
        }

        prevCamPosition = camTransform.position;
    }
}
