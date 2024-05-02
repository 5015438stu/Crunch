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
    public Player2combat combat2;
    public PlayerCombat combat;
    public Player2Movement movement2;
    public Animator animator;
    public BoxCollider2D hitbox;
    public LayerMask enemylayers;
    public InputActionReference LightPunch;
    public InputActionReference LightKick;
    public InputActionReference HeavyPunch;
    public InputActionReference HeavyKick;

    [Header("Delays")]
    public float lastclickedtime = 0f;
    public float maxcombodelay = 1f;
    public float delaytimer = 0f;
    public float delay = 1f;

    [Header("Damage")]
    public int attackDamage = 40; //Change later for each attack
    public static int zpresses = 0;
    public float attackwait;
    public bool attacking;
    public float zp = 0f;
    public bool comboend = false;

    [Header("Knockback")]
    public float knockbackx = 0f; //Change later for each attack
    public float knockbacky = 0f; //Change later for each attack

    [Header("Blocking")]
    public bool isblocking;
    public bool blockready;
    public bool blockpriming;
    public Transform blockcheck;
    public Vector2 blockarea = new Vector2(0.5F, 0.05f);

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        combat = GameObject.FindWithTag("P1").GetComponent<PlayerCombat>();
        health = GameObject.FindWithTag("P1").GetComponent<HealthScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Blockcheck();

        if (movement2.hors <= .9)
        {
            blockready = false;
            
        }
        else
        {
            blockready = true;
        }

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (comboend)
        {
            delaytimer += 1 * Time.deltaTime;
            zpresses = 0;
            zp = 0;

            if (delaytimer >= delay)
            {
                comboend = false;
                Debug.Log("attack Delay");
                delaytimer = 0;
            }
        }
        if (comboend == false)
        {
            
        }
        if (attacking)
        {
            movement2.movespeed = 6f;

            if (movement2.isjumping == false)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            }

            lastclickedtime += 1 * Time.deltaTime;

            if (lastclickedtime >= maxcombodelay)
            {
                lastclickedtime = 0;
                Debug.Log("attack Timeout");
                zpresses = 0;
                zp = 0;
                attacking = false;
            }
        }
        if (attacking == false)
        {
            movement2.movespeed = 8f;
        }

        
    }
    public void Blockcheck()
    {
        if (Physics2D.OverlapBox(blockcheck.position, blockarea, 0, enemylayers))
        {
            blockpriming = true;

            if (blockready && blockpriming && combat.attacking) /// blocking
            {
                Debug.Log("GetBlockedBozo");
                animator.SetBool("IsBlocking", true);
            }
            else
            {
                animator.SetBool("IsBlocking", false);
            }

            if (blockready && blockpriming && combat.attacking && movement2.iscrouching)
            {
                Debug.Log("GetCBlockedBozo");
                animator.SetBool("IsCrouchBlocking", true);
            }
            else
            {
                animator.SetBool("IsCrouchBlocking", false);
            }
        }
        
    }
    public void OnLightKick(InputAction.CallbackContext context)
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;

        if (context.performed)
        {
            if (comboend == false)
            {
                zpresses++;
                zp++;

                if (movement2.isjumping == true)
                {
                    attacking = true;
                    Debug.Log("AK 1");
                    animator.SetTrigger("AK1");
                    knockbackx = 50f;
                    knockbacky = 10f;
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
                        knockbacky = 20f;
                        comboend = false;
                    }

                    if (zpresses == 2)
                    {
                        attacking = true;
                        Debug.Log("Kack 2");
                        animator.SetTrigger("LK2");
                        knockbackx = 30f;
                        knockbacky = 30f;
                    }

                    if (zpresses == 3)
                    {
                        attacking = true;
                        Debug.Log("Kack 3");
                        animator.SetTrigger("LK3");
                        comboend = true;
                        knockbackx = 100f;
                        knockbacky = 40f;
                    }
                    if (zpresses == 4)
                    {
                        attacking = false;
                    }
                }
                else
                {
                    return;
                }
            }

            if (context.canceled)
            {
                attacking = false;
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            }
        }
    }
    public void OnLightPunch(InputAction.CallbackContext context)
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        

        if (context.performed)
        {
            if (comboend == false)
            {
                if (comboend == false)
                {


                }
                if (movement2.isjumping == true)
                {
                    attacking = true;
                    Debug.Log("AK 1");
                    animator.SetTrigger("AK1");
                    knockbackx = 40f;
                    knockbacky = 50f;
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
                        knockbacky = 30f;
                        comboend = false;
                    }

                    if (zpresses == 2)
                    {
                        attacking = true;
                        Debug.Log("Pawnch 2");
                        animator.SetTrigger("LP2");
                        knockbackx = 25f;
                        knockbacky = 20f;
                    }

                    if (zpresses == 3)
                    {
                        Debug.Log("Pawnch 3");
                        animator.SetTrigger("LP3");
                        comboend = true;
                        knockbackx = 40f;
                        knockbacky = 50f;
                        lastclickedtime = .9f;
                    }
                    if (zpresses == 4)
                    {
                        attacking = false;
                    }
                    
                }
                else
                {
                    return;
                }
            }

        }

        if (context.canceled)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if(collision.gameObject.tag == "P1")
        {
            if (attacking && combat.blockready == false)
            {
                Debug.Log("Inflicted Damage");
                health.takedamage(attackDamage);
                rb.AddForce(new Vector2(knockbackx, knockbacky), ForceMode2D.Impulse);
            }
            if (attacking && combat.blockready == true)
            {
                FindObjectOfType<SoundManager>().Play("BigThuddy1");
                ///stun
            }
            if (attacking && combat2.attacking == true)
            {
                Debug.Log("clash");
                rb.AddForce(new Vector2(knockbackx, 0), ForceMode2D.Impulse);
                ///stun
            }
        }*/

        if (collision.gameObject.tag == "P1")
        {
            if (attacking && combat.attacking == false) //normal hit
            {
                Debug.Log("Inflicted Damage");
                health.takedamage(attackDamage);
                rb.AddForce(new Vector2(knockbackx, knockbacky), ForceMode2D.Impulse);
            }
            if (attacking && combat.attacking == true) //clashing
            {
                rb.AddForce(new Vector2(knockbackx, 0), ForceMode2D.Impulse);
                Debug.Log("clash");
            }
            if (blockready && blockpriming)
            {
                FindObjectOfType<SoundManager>().Play("BigThuddy1");
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(blockcheck.position, blockarea);
    }
}
