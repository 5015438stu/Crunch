using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public float speed = 280;
    public float jumpforce = 15;
    public float jumpheight = 3;
    public float gravityScale = 4;
    public float fallgravityscale = 6;
    public float buttontime = 0.3f;
    public float cancelrate = 100;
    public float jumpamount = 1;
    public bool grounded = false;
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
        } else
        {
            grounded = false;
        }
    }
}    