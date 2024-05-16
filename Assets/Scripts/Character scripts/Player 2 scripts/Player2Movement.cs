using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Player2Movement : MonoBehaviour
{
    [Header("Movement")]
    public float movespeed = 8f;
    public float hors;
    public float xvelo;

    [Header("Jumping")]
    public float yvelo;
    public float jumptime;
    public float jumplength = 0.3f;
    public float jumpforce = 15f;
    public int maxjumps = 1;
    public bool isjumping = false;
    public int jumpsremaining;
    public bool canjump = true;

    [Header("Crouching")]
    public bool iscrouching = false;

    [Header("GroundCheck")]
    public Transform groundcheck;
    public LayerMask ground;
    public Vector2 groundchecksize = new Vector2(0.5f, 0.05f);

    [Header("Refs")]
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer sprite;
    public InputActionReference move;
    public InputActionReference jump;
    public InputActionReference crouch;
    public Player2BiggeHealth health2;

    [Header("Fliping")]
    public float P1xpos;
    public bool flipped;

    public void Start()
    {
        health2 = GetComponent<Player2BiggeHealth>();
        isjumping = false;
    }
    private void Update()
    {
        if (health2.hurt == true)
        {
            Debug.Log("Stun2");
            return;
        }
        else
        {
            rb.velocity = new Vector2(hors * movespeed, rb.velocity.y);
        }

        if (transform.rotation != Quaternion.Euler(0, 0, 0))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        yvelo = rb.velocity.y;
        xvelo = rb.velocity.x;

        animator.SetFloat("Speed", rb.velocity.x);

        GroundCheck();

        P1xpos = GameObject.FindWithTag("P1").transform.position.x;

        if (rb.velocity.y > 1.5)
        {
            isjumping = true;
            jumpsremaining = 0;
            animator.SetBool("IsJumping", true);
            animator.SetBool("IsFalling", false);
        }
        if (rb.velocity.y < -1)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", true);
        }

        if (P1xpos < transform.position.x)
        {
            sprite.flipX = true;
            flipped = true;
        }
        if (P1xpos > transform.position.x)
        {
            sprite.flipX = false;
            flipped = false;
        }

        if (hors == 1)
        {
            movespeed = 5f;
        }
        else if (hors == -1 && flipped)
        {
            movespeed = 5f;
        }

        if (iscrouching)
        {
            movespeed = 0f;
        }
    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            movespeed = 0f;
            animator.SetBool("IsCrouching", true);
            iscrouching = true;
        }
        if (context.canceled)
        {
            movespeed = 8f;
            animator.SetBool("IsCrouching", false);
            iscrouching = false;
        }
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

            isjumping = false;
            animator.SetBool("IsFalling", false);
            animator.SetBool("IsJumping", false);
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (canjump)
        {
            if (jumpsremaining > 0)
            {
                if (context.started)
                {
                    //full power full hold
                    Debug.Log("Jump Started");
                    rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                    jumpsremaining--;
                    isjumping = true;
                    FindObjectOfType<SoundManager>().Play("JumpSFX");
                    animator.SetBool("IsJumping", true);
                    animator.SetBool("IsFalling", false);
                }

                if (context.performed)
                {
                    Debug.Log("Jump Performed");
                }

                if (context.canceled)
                {
                    //small jump light tap
                    Debug.Log("Jump Ended");
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                    animator.SetBool("IsFalling", true);
                    animator.SetBool("IsJumping", false);
                    jumpsremaining--;
                    isjumping = true;
                }
            }
        }
        

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundcheck.position, groundchecksize);
    }
    //add jumping and moving.
}