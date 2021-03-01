using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float JumpForce = 800.0f;
    public float Speed = 1.0f;
    public float dashTime;
    public float dashSpeed;
    public float distanceBetweenImages;
    public float dashCooldown;

    [Range(0.0f, 1000.0f)]
    public float ForceClamp = 0.0f;

    public GameObject Ground;

    private Rigidbody2D rBody;
    private Animator animator;
    private bool grounded = true;
    private bool isDashing = false;
    private float dashTimeLeft;
    private float lastImageXpos;
    private float lastDash = -100;
    private float facingDirection;

    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        print("Collided");
        //print(collision.gameObject.name);
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
            //Debug.Log("TEST");
            animator.SetBool("Landed", true);
        }
    }

    void Update()
    {
        Debug.Log(dashTimeLeft);
        facingDirection = Input.GetAxisRaw("Horizontal");
        float hForce = facingDirection * Speed * Time.deltaTime;

        if (hForce == 0.0f)
        {
            rBody.velocity = new Vector2(0, rBody.velocity.y);
            animator.SetFloat("Speed", 0.0f);
        } 
        else
        {
            rBody.AddForce(Vector2.right * hForce);
            animator.SetFloat("Speed", Mathf.Abs(hForce));

            if (hForce > 0.0f)
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            } 
            else if (hForce < 0.0f)
            {
                transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
            }
        }

        rBody.velocity = new Vector2(Mathf.Clamp(rBody.velocity.x, ForceClamp * -1, ForceClamp), rBody.velocity.y);

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) && grounded)
        {
            rBody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            grounded = false;
            animator.SetBool("Landed", false);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            //print("Atempting to dash");
            if (Time.time >= (lastDash + dashCooldown))
            {
                AttemptToDash();
            }
        }
        //lol

        CheckDash();

        animator.SetFloat("YVelocity", rBody.velocity.y);
    }

    private void AttemptToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;
        print("Atempting to dash");

        PlayerAfterImagePool.Instance.GetFromPool();
        lastImageXpos = transform.position.x;
    }

    private void CheckDash()
    {
        if (isDashing)
        {
            if(dashTimeLeft > 0)
            {
                //canMove = false;
                //canFlip = false;
                rBody.velocity = new Vector2(dashSpeed * facingDirection, rBody.velocity.y);
                dashTimeLeft -= Time.deltaTime;

                if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
                {
                    PlayerAfterImagePool.Instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                }
            }

            if(dashTimeLeft <= 0)
            {
                isDashing = false;
                //canMove = true;
                //canFLip = true;
            }
        }
    }
}
