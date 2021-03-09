using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;

    public int maxHealth = 100;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        // Play hurt animation
        if (currentHealth <= 0)
        {
            Die();
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
}
