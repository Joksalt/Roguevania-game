using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollide : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerData pd;
    public GameObject player;

    public bool useIntegerToLoadLevel = false;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        player = collision.gameObject;
        if (player.name == "Player")
        {
            pd.movementVelocity = 5.0f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        player = collision.gameObject;
        if (player.name == "Player")
        {
            pd.movementVelocity = 10.0f;
        }
    }
}
