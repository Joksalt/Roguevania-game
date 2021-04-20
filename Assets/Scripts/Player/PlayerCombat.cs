using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 40;


    private float[] attackDetails = new float[2];

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
            Debug.Log($"Damage amount: {attackDamage}");
        }
    }

    void Attack()
    {
        // Play attack animation
        animator.SetTrigger("attackTemp");
        animator.SetBool("isAttacking", true);
        Debug.Log("Attack");
        // Detect enemies that are in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        attackDetails[0] = 40;
        attackDetails[1] = transform.position.x;
        // Damage them
        foreach(Collider2D enemy in hitEnemies)
        {

            Enemy comp = enemy.GetComponent<Enemy>();
            if (comp != null)
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
            
            enemy.transform.parent.SendMessage("Damage", attackDetails);
        }

        
    }

    void FinnishAttack()
    {
        animator.SetBool("isAttacking", false);
        animator.SetBool("attack", false);
        Debug.Log("Attack end");
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
