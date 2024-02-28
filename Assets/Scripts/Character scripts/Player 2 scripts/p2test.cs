using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class p2test : MonoBehaviour
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

    private void Update()
    {
        rb.velocity = new Vector2(hors * movespeed, rb.velocity.y);
        animator.SetFloat("Speed", rb.velocity.x);
        GroundCheck();
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
}