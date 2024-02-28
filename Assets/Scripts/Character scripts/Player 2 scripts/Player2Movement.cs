using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player2Movement : MonoBehaviour
{
    [Header("Movement")]
    private bool isfacingright = true;
    private float movespeed = 8f;
    private float hors;

    [Header("Jumping")]
    public float jumptime;
    public float jumplength = 0.3f;
    public float jumpforce = 15f;
    public int maxjumps = 1;
    int jumpsremaining;

    [Header("GroundCheck")]
    public Transform groundcheck;
    public LayerMask ground;
    public Vector2 groundchecksize = new Vector2(0.5f, 0.05f);

    [Header("Refs")]
    public Rigidbody2D rb;
    public Animator animator;
    public InputActionReference move;
    public InputActionReference jump;
    public InputActionReference crouch;

    [Header("Gravity")]
    public float basegrav = 2f;
    public float maxfallspeed = 18f;
    public float fallspeedmultiplier = 2f;

    private void Update()
    {
        rb.velocity = new Vector2(hors * movespeed, rb.velocity.y);
        animator.SetFloat("Speed", rb.velocity.x);
        GroundCheck();
        Gravity();
    }

    private void Gravity()
    {
        rb.gravityScale = basegrav * fallspeedmultiplier;
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxfallspeed));
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        hors = context.ReadValue<Vector2>().x;
    }
    public void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundcheck.position, groundchecksize, 0, ground))
        {
            jumpsremaining = maxjumps;
            animator.SetBool("IsFalling", false);
            animator.SetBool("IsJumping", false);
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (jumpsremaining > 0)
        {
            if (context.performed)
            {
                //full power full hold
                Debug.Log("Jump Started");
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                animator.SetBool("IsJumping", true);
                animator.SetBool("IsFalling", false);
                jumpsremaining--;
            }

            if (context.canceled)
            {
                //small jump light tap
                Debug.Log("Jump Ended");
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                animator.SetBool("IsFalling", true);
                animator.SetBool("IsJumping", false);
                jumpsremaining--;
            }
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundcheck.position, groundchecksize);
    }
    //add jumping and moving.

    //add jumping and moving.
    /*public Animator animator;
    public float speed = 280; //walk
    public float jumpforce = 15; //jump
    public float jumpheight = 3; //jump
    public float gravityScale = 4; //jump
    public float fallgravityscale = 6; //jump
    public float buttontime = 0.3f; //jump
    public float cancelrate = 100; //jump
    public float jumpamount = 1; //jump
    public bool grounded = false; //jump
    //bool crouching = false;

    Vector2 move;

    Rigidbody2D rb;

    bool jumping;
    float jumptime;
    bool jumpcancelled;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(move.x * speed * Time.deltaTime, rb.velocity.y);
        if (jumpcancelled && jumping && rb.velocity.y > 0)
        {
            rb.AddForce(Vector2.down * cancelrate);
        }
    }

    private void Update()
    {
        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); //walking

        animator.SetFloat("Speed", move.x);

        if (Input.GetKeyDown(KeyCode.Space) && jumpamount == 1) //jumping
        {
            animator.SetBool("IsJumping", true);
            animator.SetBool("IsFalling", false);

            rb.gravityScale = gravityScale;
            float jumpForce = Mathf.Sqrt(jumpheight * -2 * (Physics2D.gravity.y * rb.gravityScale));
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jumpcancelled = false;
            jumping = true;
            jumptime = 0;
            jumpamount = 0;
        }
        else
        {
            //nothing
        }

        if (Input.GetKeyUp(KeyCode.S)) //Crouching stuff
        {
            speed = 280;
            animator.SetBool("IsCrouching", false);
            //crouching = false;
        }

        //Add crouchblock

        if (Input.GetKeyDown(KeyCode.S)) //Crouching stuff
        {
            speed = 0;
            animator.SetBool("IsCrouching", true);
            //crouching = true;
        }

        if (jumping) //settings for jumptime
        {
            jumptime += Time.deltaTime;
            if (Input.GetKeyUp(KeyCode.Space))
            {
                jumpcancelled = true;
            }
            if (jumptime > buttontime)
            {
                jumping = false;
                animator.SetBool("IsFalling", true);
                animator.SetBool("IsJumping", false);

            }
        }

    }
    public void OnCollisionEnter2D(Collision2D collision) // more jump stuff
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
            jumpamount = 1;
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", false);
        }
        else
        {
            grounded = false;
        }
    }*/
}