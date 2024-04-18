using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [Header("Refs")]
    public Player2BiggeHealth health2;
    public Rigidbody2D rb;
    public PlayerMovement movement;
    public DummyScript dummy;
    public Animator animator;
    public BoxCollider2D hitbox;
    public LayerMask enemylayers;
    public InputActionReference LightPunch;
    public InputActionReference LightKick;
    public InputActionReference HeavyPunch;
    public InputActionReference HeavyKick;

    [Header("Knockback")]
    public int attackDamage = 40; //Change later for each attack
    public float knockbackx = 0f; //Change later for each attack fix kb https://www.youtube.com/watch?v=Jy1yXbKYW68
    public float knockbacky = 0f; //Change later for each attack
    public float lastclickedtime = 0f;
    public float maxcombodelay = 1f;
    public static int zpresses = 0;
    public float attackwait;
    public bool attacking;
    public float zp = 0f;
    public bool comboend = false;

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

            if (lastclickedtime > maxcombodelay)
            {
                lastclickedtime = 0;
                Debug.Log("Attack Timeout");
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

            if (zpresses == 1)
            {
                attacking = true;
                Debug.Log("Kack 1");
                animator.SetTrigger("IsLKicking");
                knockbackx = -2f;
                knockbacky = 2f;
                comboend = false;
            }

            if (zpresses == 2)
            {
                attacking = true;
                Debug.Log("Kack 2");
                animator.SetTrigger("LK2");
                knockbackx = -1f;
                knockbacky = 3f;
            }

            if (zpresses == 3)
            {
                attacking = true;
                Debug.Log("Kack 3");
                animator.SetTrigger("LK3");
                comboend = true;
                knockbackx = 5f;
                knockbacky = 12f;
            }
            if (zpresses == 4)
            {
                attacking = false;
                zpresses = 0;
                zp = 0;
                attacking = false;
            }

            if (context.canceled)
            {
                attacking = false;
            }
        }
    }
    public void OnLightPunch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (movement.isjumping == true)
            {
                
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
                    knockbackx = -2f;
                    knockbacky = 2f;
                    comboend = false;
                }

                if (zpresses == 2)
                {
                    attacking = true;
                    Debug.Log("Pawnch 2");
                    animator.SetTrigger("LP2");
                    knockbackx = -1f;
                    knockbacky = 3f;
                }

                if (zpresses == 3)
                {
                    attacking = true;
                    Debug.Log("Pawnch 3");
                    animator.SetTrigger("LP3");
                    comboend = true;
                    knockbackx = 5f;
                    knockbacky = 12f;
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
            attacking = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "P2")
        {
            if (attacking)
            {
                Debug.Log("Inflicted Damage");
                health2.takedamage(attackDamage);
                health2.takekb(knockbackx);
            }
        }
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

