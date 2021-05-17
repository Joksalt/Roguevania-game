using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    private enum State
    {
        Moving,
        Knockback,
        Dead
    }

    private State currentState;

    [SerializeField]
    private float
        groundCheckDistance,
        wallCheckDistance,
        movementSpeed,
        maxHealth,
        knockbackDuration;
    [SerializeField]
    private Transform
        groundCheck,
        wallCheck;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private Vector2 knockbackSpeed;
    [SerializeField]
    private GameObject
        hitParticle,
        deathChunkParticle,
        deathBloodParticle,
        hamPrefab;

    public int damage = 5;
    public int GoldWorth = 10;

    private float 
        currentHealth,
        knockbackStartTime;

    private int 
        facingDirection,
        damageDirection;

    private Vector2 movement;

    private bool
        groundDetected,
        wallDetected;

    private Rigidbody2D aliveRb;
    private Animator aliveAnim;

    public AudioClip AttackSound;

    private void Start()
    {
        aliveRb = GetComponent<Rigidbody2D>();
        aliveAnim = GetComponent<Animator>();

        currentHealth = maxHealth;
        facingDirection = 1;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Moving:
                UpdateMovingState();
                break;
            case State.Knockback:
                UpdateKnockbackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
    }

    private void EnterMovingState()
    {

    }

    private void UpdateMovingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

        if(!groundDetected || wallDetected)
        {
            Flip();
        }
        else
        {
            // Target the player
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            
            if (player.transform.position.x < this.transform.position.x)
            {
                if (facingDirection != -1)
                {
                    Flip();
                }
            }
            else
            {
                if (facingDirection != 1)
                {
                    Flip();
                }
            }

            movement.Set(movementSpeed * facingDirection, aliveRb.velocity.y);
            aliveRb.velocity = movement;
        }
    }

    private void ExitMovingState()
    {

    }

    private void EnterKnockbackState()
    {
        knockbackStartTime = Time.time;
        movement.Set(knockbackSpeed.x * damageDirection * 2, knockbackSpeed.y);
        aliveRb.velocity = movement;
        aliveAnim.SetBool("Knockback", true);
    }

    private void UpdateKnockbackState()
    {
        if(Time.time >= knockbackStartTime + knockbackDuration)
        {
            SwitchState(State.Moving);
        }
    }

    private void ExitKnockbackState()
    {
        aliveAnim.SetBool("Knockback", false);
    }

    private void EnterDeadState()
    {
        Instantiate(deathChunkParticle, transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
        Instantiate(hamPrefab, transform.position, hamPrefab.transform.rotation);
        Destroy(gameObject);
    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }

    private void OnPlayerHit(Player player)
    {
        currentHealth -= player.playerData.AttackDamage;

        Instantiate(hitParticle, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        if(player.transform.position.x > transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }

        //Hit particle

        if(currentHealth > 0.0f)
        {
            SwitchState(State.Knockback);
        }
        else if(currentHealth <= 0.0f)
        {
            player.playerData.Gold += GoldWorth;
            SwitchState(State.Dead);
        }

        player.AudioSource.PlayOneShot(player.playerData.AudioOption("Attack").Audio);
    }

    private void OnPlayerHitRange(Player player)
    {
        currentHealth -= player.playerData.RangeDamage;

        Instantiate(hitParticle, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        GameObject playerTemp = GameObject.FindGameObjectWithTag("Player");
        if (playerTemp.transform.position.x > transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }

        //Hit particle

        if (currentHealth > 0.0f)
        {
            SwitchState(State.Knockback);
        }
        else if (currentHealth <= 0.0f)
        {
            player.playerData.Gold += GoldWorth;
            SwitchState(State.Dead);
        }

        player.AudioSource.PlayOneShot(player.playerData.AudioOption("Attack").Audio);
    }

    private void HitParticle()
    {

    }

    private void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void SwitchState(State state)
    {
        switch (currentState)
        {
            case State.Moving:
                ExitMovingState();
                break;
            case State.Knockback:
                ExitKnockbackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        switch (state)
        {
            case State.Moving:
                EnterMovingState();
                break;
            case State.Knockback:
                EnterKnockbackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        currentState = state;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player p = collision.gameObject.GetComponent<Player>();
            p.playerData.CurrentHealth -= damage;

            // Knock back
            p.RB.velocity = new Vector2(knockbackSpeed.x * facingDirection, knockbackSpeed.y);
            p.AudioSource.PlayOneShot(AttackSound);
        }
    }
}
