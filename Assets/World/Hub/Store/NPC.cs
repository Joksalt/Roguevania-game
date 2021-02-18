using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            onPlayerTouch(collision.gameObject);
        }
    }

    void onPlayerTouch(GameObject player)
    {
        player.GetComponent<Player>().CurrentHealth -= 10;
    }
}
