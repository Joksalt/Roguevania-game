using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{

    public float Speed = 20f;
    public int Damage = 40;
    public Player playerObj;
    [SerializeField]
    public Rigidbody2D rb;

    [SerializeField]
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        playerObj = player.GetComponent<Player>();
        rb.velocity = transform.right * Speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //Enemy enemy = hitInfo.GetComponent<Enemy>();

        if(hitInfo.tag == "Enemy")
        {
            hitInfo.transform.gameObject.SendMessage("OnPlayerHitRange", playerObj, SendMessageOptions.DontRequireReceiver);
        }

        if(hitInfo.tag != "Player")
        {
            Destroy(gameObject);
        }

    }


    public void Init(Vector3 direction)
    {
        rb.AddForce(direction);
    }
}
