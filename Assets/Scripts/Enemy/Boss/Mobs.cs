using System.Collections;
using UnityEngine;
[RequireComponent(typeof(CanvasGroup))]

public class Mobs : MonoBehaviour
{
	private enum State
	{
		Moving,
		Knockback,
		Dead
	}

	private State currentState;

	static int AnimatorWalk = Animator.StringToHash("Walk");
	static int AnimatorAttack = Animator.StringToHash("Attack");
	Animator _animator;

	[SerializeField]
	private float
		groundCheckDistance,
		wallCheckDistance,
		movementSpeed,
		knockbackDuration;
	public float
		maxHealth;

	public float
		currentHealth;
	private float
		knockbackStartTime;

	private int
		facingDirection,
		damageDirection;

	[SerializeField]
	private Transform
		groundCheck,
		wallCheck;

	private Vector2 movement;

	private bool
		groundDetected,
		wallDetected;

	[SerializeField]
	private LayerMask whatIsGround;
	[SerializeField]
	private Vector2 knockbackSpeed;
	[SerializeField]
	private GameObject
		hitParticle,
		deathChunkParticle,
		deathBloodParticle;

	private Rigidbody2D aliveRb;

	public int damage = 5;
	public int GoldWorth = 10;

	public AudioClip AttackSound;

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

	void Start()
	{
		aliveRb = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();

		currentHealth = maxHealth;
		facingDirection = -1;
	}

	private void EnterMovingState()
	{

	}

	private void UpdateMovingState()
	{
		groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
		wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

		if (wallDetected)
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
        //aliveAnim.SetBool("Knockback", true);
    }

	private void UpdateKnockbackState()
	{
		if (Time.time >= knockbackStartTime + knockbackDuration)
		{
			SwitchState(State.Moving);
		}
	}

	private void ExitKnockbackState()
	{
        //aliveAnim.SetBool("Knockback", false);
    }

	private void EnterDeadState()
	{
		Instantiate(deathChunkParticle, transform.position, deathChunkParticle.transform.rotation);
		Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
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

		Vector3 position = transform.position;
		position.y += 0.75f;
		position.x += 1.0f * facingDirection;

		Instantiate(hitParticle, position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

		if (player.transform.position.x > transform.position.x)
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


		Debug.Log("attacked for:" + player.playerData.AttackDamage.ToString());
		Debug.Log("current mob health:" + currentHealth.ToString());
		player.AudioSource.PlayOneShot(player.playerData.AudioOption("Attack").Audio);
	}

	private void Flip()
	{
		Debug.Log("Called flip");
		facingDirection *= -1;
		transform.Rotate(0.0f, 180.0f, 0.0f);
	}

	IEnumerator Animate()
	{
		yield return new WaitForSeconds(5f);
		while (true)
		{
			_animator.SetBool(AnimatorWalk, true);
			yield return new WaitForSeconds(1f);

			_animator.transform.localScale = new Vector3(30, 30, 1);
			yield return new WaitForSeconds(1f);

			_animator.SetBool(AnimatorWalk, false);
			yield return new WaitForSeconds(1f);

			_animator.SetTrigger(AnimatorAttack);
			yield return new WaitForSeconds(1f);

			_animator.SetTrigger(AnimatorAttack);
			yield return new WaitForSeconds(1f);

			_animator.SetTrigger(AnimatorAttack);
			yield return new WaitForSeconds(5f);
		}
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


	public void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			Player p = collision.gameObject.GetComponent<Player>();
			p.playerData.CurrentHealth -= damage;

			// Knock back
			p.RB.velocity = new Vector2(knockbackSpeed.x * facingDirection, knockbackSpeed.y);
			p.AudioSource.PlayOneShot(AttackSound);
			_animator.SetTrigger(AnimatorAttack);
		}
	}
}
