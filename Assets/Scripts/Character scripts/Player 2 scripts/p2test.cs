using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class p2test : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundcheck;
    public LayerMask ground;

    public Animator animator;

    public float jumpforce = 15f;

    private bool isfacingright = true;
    private float movespeed = 280;

    public float gravityScale = 4; //jump
    public float fallgravityscale = 6; //jump
    public float buttontime = 0.3f; //jump
    public float cancelrate = 100; //jump
    public float jumpamount = 1; //jump
    public bool jumppressed;
    public float jumptime;
    public bool jumpcancelled;
    public float jumpheight = 5f;

    private Vector2 movedirection;

    public InputActionReference move;
    public InputActionReference jump;
    public InputActionReference crouch;

    private void Update()
    {
        movedirection = move.action.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movedirection.x * movespeed * Time.deltaTime, movedirection.y);
        rb.AddForce(Vector2.down * cancelrate);
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundcheck.position, 0.2f, ground);

    }
    public void OnJump()
    {
        Debug.Log("jump");

    }
    //add jumping and moving.
}