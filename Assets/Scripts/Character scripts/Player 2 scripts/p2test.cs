using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class p2test : MonoBehaviour
{
    public Rigidbody2D rb;

    public Animator animator;

    [SerializeField] float jumpforce;

    public float gravityScale = 4; //jump
    public float fallgravityscale = 6; //jump
    public float buttontime = 0.3f; //jump
    public float cancelrate = 100; //jump
    public float jumpamount = 1; //jump
    public bool jumppressed;
    public float jumptime;
    public bool jumpcancelled;
    public float movespeed = 280;
    public float jumpheight = 5f;
    public bool groundedplayer;
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

        /*if (jumpcancelled && jumppressed && rb.velocity.y > 0)
        {
            rb.AddForce(Vector2.down * cancelrate);
        }*/
    }

    public void OnJump()
    {
        Debug.Log("jump");
        rb.gravityScale = gravityScale;
        float jumpForce = Mathf.Sqrt(jumpheight * -2 * (Physics2D.gravity.y * rb.gravityScale));
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        jumppressed = true;
        jumptime = 0;

        jumptime += Time.deltaTime;

        if (jumptime > buttontime)
        {
            jumppressed = false;

        }
    }
    //add jumping and moving.
}