using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [Header("Refs")]
    public Player2combat combat2;
    public Player2BiggeHealth health2;
    public Rigidbody2D rb;
    public PlayerMovement movement;
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

    [Header("Knockback")]
    public float knockbackx = 0f; //Change later for each attack fix kb https://www.youtube.com/watch?v=Jy1yXbKYW68
    public float knockbacky = 0f; //Change later for each attack

    [Header("Damage")]
    public int attackDamage = 40; //Change later for each attack
    public static int zpresses = 0;
    public float attackwait;
    public bool attacking;
    public float zp = 0f;
    public bool comboend = false;

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
        movement = GetComponent<PlayerMovement>();
        combat2 = GameObject.FindWithTag("P2").GetComponent<Player2combat>();
        health2 = GameObject.FindWithTag("P2").GetComponent<Player2BiggeHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        Blockcheck();

        if (movement.hors == -1)
        {
            blockready = true;
        }
        else
        {
            blockready = false;
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
            movement.movespeed = 4f;
            if (movement.isjumping == false)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            } 

            lastclickedtime += 1 * Time.deltaTime;

            if (lastclickedtime > maxcombodelay)
            {
                lastclickedtime = 0;
                Debug.Log("Attack Timeout");
                zpresses = 0;
                zp = 0;
                attacking = false;
            }
        }
        if (attacking == false)
        {
            movement.movespeed = 8f;
        }
    }
    public void Blockcheck()
    {
        if (Physics2D.OverlapBox(blockcheck.position, blockarea, 0, enemylayers))
        {
            blockpriming = true;

            if (blockready && blockpriming && combat2.attacking && movement.iscrouching == false)
            {
                Debug.Log("GetBlockedBozo");
                animator.SetBool("IsBlocking", true);

            }
            else
            {
                animator.SetBool("IsBlocking", false);
            }

            if (blockready && blockpriming && combat2.attacking && movement.iscrouching)
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
        if (context.performed)
        {
            if (comboend == false)
            {
                zpresses++;
                zp++;

                if (movement.isjumping == true)
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
                if (movement.isjumping == false)
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
            }
        }
    }
    public void OnLightPunch(InputAction.CallbackContext context)
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;

        if (context.performed)
        {
            if (context.performed)
            {
                if (comboend == false)
                {
                    if (comboend == false)
                    {


                    }
                    if (movement.isjumping == true)
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
                    if (movement.isjumping == false)
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
                        }

                    }
                    else
                    {
                        return;
                    }
                }

            }

        }

        if (context.canceled)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            Debug.Log("YfreezeOFF");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "P2")
        {
            if (attacking && combat2.attacking == false)
            {
                Debug.Log("Inflicted Damage");
                health2.takedamage(attackDamage);
                rb.AddForce(new Vector2(knockbackx, knockbacky), ForceMode2D.Impulse);
            }
            if (attacking && combat2.attacking == true)
            {
                Debug.Log("clash");
                rb.AddForce(new Vector2(knockbackx, 0), ForceMode2D.Impulse);
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
    /*
        public Animator animator;

        public BoxCollider2D hitbox;

        public LayerMask enemylayers;
        public int attackDamage = 40; //Change later for each attack
        public float knockbackx = 0f; //Change later for each attack
        public float knockbacky = 0f; //Change later for each attack
        public float lastclickedtime = 0f;
        public float maxcombodelay = 1f;
        public static int zpresses = 0;
        public float attackwait;
        public bool attacking;
        public float zp = 0f;
        public bool comboend = false;

        Rigidbody2D rb;
        DummyScript dummy;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            dummy = GetComponent<DummyScript>();
            hitbox = GetComponent<BoxCollider2D>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (attacking)
            {
                lastclickedtime += 1 * Time.deltaTime;

                if (lastclickedtime > maxcombodelay)
                {
                    lastclickedtime =
                        0;
                    Debug.Log("punch Timeout");
                    zpresses = 0;
                    zp = 0;
                    attacking = false;
                }
            }

            if (zpresses == 4)
            {
                lastclickedtime = 0;
                Debug.Log("punch fakeout");
                zpresses = 0;
                zp = 0;
                attacking = false;
            }

            ///Punching
            if (Input.GetKeyDown(KeyCode.Z))
            {
                lastclickedtime = Time.deltaTime;
                zpresses++;
                zp++;

                if (zpresses == 1)
                {
                    comboend = false;
                    lightpunch1();
                }

                if (zpresses == 2)
                {
                    lightpunch2();
                }

                if (zpresses == 3)
                {
                    lightpunch3();
                }
            }
            //Kicking
            if (Input.GetKeyDown(KeyCode.X))
            {
                attacking = true;
                animator.SetTrigger("IsLKicking");
            }

            //Add blocking and air attacks
        }
        public void lightpunch1()
        {
            attacking = true;
            Debug.Log("Pawnch 1");
            animator.SetTrigger("LP1");
            knockbackx = -2f;
            knockbacky = 2f;
        }
        public void lightpunch2()
        {
            attacking = true;
            Debug.Log("Pawnch 2");
            animator.SetTrigger("LP2");
            knockbackx = -1f;
            knockbacky = 3f;
        }
        public void lightpunch3()
        {
            attacking = true;
            Debug.Log("Pawnch 3");
            animator.SetTrigger("LP3");
            comboend = true;
            zpresses = 0;
            zp = 0;
            knockbackx = 5f;
            knockbacky = 12f;
            lastclickedtime =+ 0.8f;
        }
    */
}

