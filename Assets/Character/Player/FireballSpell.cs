using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpell : MonoBehaviour
{
    public Transform firePoint;
    public Transform firePointUp;
    public Transform firePointDown;
    public Transform firePointLeft;
    public Transform firePointRight;
    public Transform firePointUL;
    public Transform firePointUR;
    public Transform firePointDL;
    public Transform firePointDR;
    public GameObject fireBallPrefab;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(fireBallPrefab, firePoint.position, firePoint.rotation);
    }

    public void Scatter()
    {
        Instantiate(fireBallPrefab, firePointUp.position, firePointUp.rotation);
        Instantiate(fireBallPrefab, firePointDown.position, firePointDown.rotation);
        Instantiate(fireBallPrefab, firePointLeft.position, firePointLeft.rotation);
        Instantiate(fireBallPrefab, firePointRight.position, firePointRight.rotation);
        Instantiate(fireBallPrefab, firePointUL.position, firePointUL.rotation);
        Instantiate(fireBallPrefab, firePointUR.position, firePointUR.rotation);
        Instantiate(fireBallPrefab, firePointDL.position, firePointDL.rotation);
        Instantiate(fireBallPrefab, firePointDR.position, firePointDR.rotation);
        print("TESSSSST");
    }
}
