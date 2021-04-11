using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform player;
    private Rigidbody2D m_Rigidbody2D;
    private Animator animator;

    public int maxHealth = 100;
    int currentHealth;
    public float force;
    public float stoppingDistance = 1.5f;
    //public float m_JumpForce = 250;
    private bool m_FacingRight = true;
    //private bool jump = false;
    private float max_walking_velocity = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().transform;
    }

    private void FixedUpdate()
    {
        FollowPLayer();
    }

    private void LateUpdate()
    {
        SpriteFlip();
    }

    private void FollowPLayer()
    {
        if (player.position.x > transform.position.x + stoppingDistance)
        {
            if (m_Rigidbody2D.velocity.x < max_walking_velocity)
                m_Rigidbody2D.AddForce(new Vector2(force * Time.deltaTime, 0f));
        }
        else if (player.position.x < transform.position.x - stoppingDistance)
        {
            //Debug.Log(m_Rigidbody2D.velocity);

            if (m_Rigidbody2D.velocity.x > -max_walking_velocity)
                m_Rigidbody2D.AddForce(new Vector2(-force * Time.deltaTime, 0f));
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        animator.SetTrigger("Hurt");

        // Play hurt animation
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            Debug.Log("Player attacked");
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        // Die animation
        animator.SetBool("IsDead", true);
        // Disable the enemy
        GetComponent<Collider2D>().enabled = false;

        this.enabled = false; 
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length - 0.25f);
    }

    private void SpriteFlip()
    {
        if (player.position.x < transform.position.x && !m_FacingRight)
        {
            Flip();
        }
        else if (player.position.x > transform.position.x && m_FacingRight)
        {
            Flip();
        }
    }
    private void Flip()
    {
        m_FacingRight = !m_FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
