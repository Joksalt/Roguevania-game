using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAI : MonoBehaviour
{
    public Transform target;

    [SerializeField]
    private Vector2 knockbackSpeed;

    [SerializeField]
    private int damage = 2;
    private int facingDirection;

    [SerializeField]
    public GameObject hitParticle;

    public AudioClip AttackSound;
    public AudioClip DeathSound;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    private int GoldWorth = 1;

    // Start is called before the first frame update
    void Start()
    {
        facingDirection = 1;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (force.x >= 0.01f)
        {
            facingDirection = 1;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (force.x <= -0.01f)
        {
            facingDirection = -1;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void OnPlayerHit(Player player)
    {
        Instantiate(hitParticle, transform.position, Quaternion.Euler(0.0f, 0.0f, UnityEngine.Random.Range(0.0f, 360.0f)));
        player.AudioSource.PlayOneShot(player.playerData.AudioOption("Attack").Audio);
        Die(player);
    }

    private void OnPlayerHitRange(Player player)
    {
        Instantiate(hitParticle, transform.position, Quaternion.Euler(0.0f, 0.0f, UnityEngine.Random.Range(0.0f, 360.0f)));
        Die(player);
    }

    private void Die(Player player)
    {
        player.playerData.Gold += GoldWorth;
        Player p = player.gameObject.GetComponent<Player>();
        p.AudioSource.PlayOneShot(DeathSound);
        Destroy(gameObject);
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
