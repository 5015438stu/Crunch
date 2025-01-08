using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
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
    public HealthScript health;

    [Header("Fliping")]
    public float P2xpos;
    public bool flipped;

    public void Start()
    {
        isjumping = false;
        health = GetComponent<HealthScript>();
    }
    void Update()
    {
        if (health.hurt == true)
        {
            Debug.Log("Stun1");
            return;
        }
        else
        {
            rb.linearVelocity = new Vector2(hors * movespeed, rb.linearVelocity.y);
        }

        if (transform.rotation != Quaternion.Euler(0, 0, 0))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }


        xvelo = rb.linearVelocity.x;
        yvelo = rb.linearVelocity.y;

        animator.SetFloat("Speed", rb.linearVelocity.x);

        GroundCheck();

        P2xpos = GameObject.FindWithTag("P2").transform.position.x;

        if (rb.linearVelocity.y > 1.5)
        {
            isjumping = true;
            jumpsremaining = 0;
            animator.SetBool("IsJumping", true);
            animator.SetBool("IsFalling", false);
        }
        if (rb.linearVelocity.y < -1)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", true);
        }

        if (P2xpos < transform.position.x)
        {
            sprite.flipX = true;
            flipped = true;
        }
        if (P2xpos > transform.position.x)
        {
            sprite.flipX = false;
            flipped = false;
        }

        if (hors == -1)
        {
            movespeed = 5f;
        }
        else
        {
            movespeed = 8f;
        }

        if (hors == 1 && flipped)
        {
            movespeed = 5f;
        }
        else if (hors == -1 && flipped)
        {
            movespeed = 8f;
        }


        if (iscrouching)
        {
            movespeed = 0f;
        }

    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetBool("IsCrouching", true);
            iscrouching = true;
        }
        if (context.canceled)
        {
            animator.SetBool("IsCrouching", false);
            iscrouching = false;
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {

        FindObjectOfType<SoundManager>().Play("Walk");
        hors = context.ReadValue<Vector2>().x;

        if (context.canceled)
        {
            FindObjectOfType<SoundManager>().Pause("Walk");
        }
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
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpforce);
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
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
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