using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2combat : MonoBehaviour
{
    [Header("Refs")]
    public HealthScript health;
    public Rigidbody2D rb;
    public DummyScript dummy;
    public Player2Movement movement2;
    public Animator animator;
    public BoxCollider2D hitbox;
    public LayerMask enemylayers;
    public InputActionReference LightPunch;
    public InputActionReference LightKick;
    public InputActionReference HeavyPunch;
    public InputActionReference HeavyKick;

    [Header("Damage")]
    public int attackDamage = 40; //Change later for each attack
    public float lastclickedtime = 0f;
    public float maxcombodelay = 1f;
    public static int zpresses = 0;
    public float attackwait;
    public bool attacking;
    public float zp = 0f;
    public bool comboend = false;

    [Header("Knockback")]
    public float knockbackx = 0f; //Change later for each attack
    public float knockbacky = 0f; //Change later for each attack

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dummy = GetComponent<DummyScript>();
        hitbox = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking)
        {
            lastclickedtime += 1 * Time.deltaTime;

            if (lastclickedtime >= maxcombodelay)
            {
                lastclickedtime = 0;
                Debug.Log("punch Timeout");
                zpresses = 0;
                zp = 0;
                attacking = false;
            }
        }
    }
    public void OnLightKick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            zpresses++;
            zp++;

            if (movement2.isjumping == true)
            {
                attacking = true;
                Debug.Log("AK 1");
                animator.SetTrigger("AK1");
                knockbackx = 4f;
                knockbacky = 4f;
                comboend = true;
                zpresses = 0;
                zp = 0;
            }
            if (movement2.isjumping == false)
            {
                if (zpresses == 1)
                {
                    attacking = true;
                    Debug.Log("Kack 1");
                    animator.SetTrigger("IsLKicking");
                    knockbackx = 60f;
                    knockbacky = 10f;
                    comboend = false;
                }

                if (zpresses == 2)
                {
                    attacking = true;
                    Debug.Log("Kack 2");
                    animator.SetTrigger("LK2");
                    knockbackx = -2f;
                    knockbacky = 6f;
                }

                if (zpresses == 3)
                {
                    attacking = true;
                    Debug.Log("Kack 3");
                    animator.SetTrigger("LK3");
                    comboend = true;
                    knockbackx = 10f;
                    knockbacky = 24f;
                }
                if (zpresses == 4)
                {
                    attacking = false;
                    zpresses = 0;
                    zp = 0;
                    attacking = false;
                }
            }

            if (context.canceled)
            {
                attacking = false;
            }
        }
    }
    public void OnLightPunch(InputAction.CallbackContext context)
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;

        if (context.performed)
        {
            
            if (movement2.isjumping == true)
            {
                attacking = true;
                Debug.Log("AK 1");
                animator.SetTrigger("AK1");
                knockbackx = 60f;
                knockbacky = 10f;
                comboend = true;
                zpresses = 0;
                zp = 0;
            }
            if (movement2.isjumping == false)
            {
                zpresses++;
                zp++;

                if (zpresses == 1)
                {
                    attacking = true;
                    Debug.Log("Pawnch 1");
                    animator.SetTrigger("LP1");
                    knockbackx = -20f;
                    knockbacky = 20f;
                    comboend = false;
                }

                if (zpresses == 2)
                {
                    attacking = true;
                    Debug.Log("Pawnch 2");
                    animator.SetTrigger("LP2");
                    knockbackx = -30f;
                    knockbacky = 40f;
                }

                if (zpresses == 3)
                {
                    attacking = true;
                    Debug.Log("Pawnch 3");
                    animator.SetTrigger("LP3");
                    comboend = true;
                    knockbackx = 100f;
                    knockbacky = 70f;
                }
                if (zpresses == 4)
                {
                    attacking = false;
                    zpresses = 0;
                    zp = 0;
                    attacking = false;
                }
            }

        }

        if (context.canceled)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "P1")
        {
            if (attacking)
            {
                health.takedamage(attackDamage);
            }
        }
    }
}
