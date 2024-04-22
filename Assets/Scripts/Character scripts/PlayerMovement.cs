using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float movespeed = 8f;
    public float hors;

    [Header("Jumping")]
    public float yvelo;
    public float jumptime;
    public float jumplength = 0.3f;
    public float jumpforce = 15f;
    public int maxjumps = 1;
    public bool isjumping = false;
    int jumpsremaining;

    [Header("Crouching")]
    public bool iscrouching = false;

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

    public void Start()
    {
        isjumping = false;
    }
    private void Update()
    {
        yvelo = rb.velocity.y;
        rb.velocity = new Vector2(hors * movespeed, rb.velocity.y);
        animator.SetFloat("Speed", rb.velocity.x);
        GroundCheck();

        if (rb.velocity.y > 1.5)
        {
            isjumping = true;
            animator.SetBool("IsJumping", true);
            animator.SetBool("IsFalling", false);
        }
        if (rb.velocity.y < -1)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", true);
        }
    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.started)
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
        if (jumpsremaining > 0)
        {
            if (context.started)
            {
                //full power full hold
                Debug.Log("Jump Started");
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                jumpsremaining--;
                isjumping = true;
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
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundcheck.position, groundchecksize);
    }
    //add jumping and moving.
}