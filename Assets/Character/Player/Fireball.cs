using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{

    public float Speed = 20f;
    public int Damage = 40;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * Speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {

        
        Enemy enemy = hitInfo.GetComponent<Enemy>();

        if(hitInfo.tag == "Enemy")
        {
            enemy.TakeDamage(Damage);
        }

        if(enemy != null)
        {
            enemy.TakeDamage(Damage);
        }

        Destroy(gameObject);
    }
}
