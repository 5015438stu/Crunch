using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    public Image frontbar;
    public Image backbar;
    public InputActionReference LightPunch;
    public InputActionReference LightKick;
    public InputActionReference HeavyPunch;
    public InputActionReference HeavyKick;
    public InputActionReference LightCrunch;

    [Header("Delays")]
    public float lastclickedtime = 0f;
    public float maxcombodelay = 1f;
    public float delaytimer = 0f;
    public float delay = 1f;
    public bool canattack = true;

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
    public bool invs = false;

    [Header("Blocking")]
    public bool isblocking;
    public bool blockready;
    public bool blockpriming;
    public Transform blockcheck;
    public Vector2 blockarea = new Vector2(0.5F, 0.05f);

    [Header("CrunchMeter")]
    public float maxcrunch = 1000;
    public float currentcrunch;
    public float lerptimer;
    public float chipspeed = 2f;

    [Header("CrunchMoves")]
    public bool flexing;
    public float flexlength = 1.5f;
    public float flextime;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        combat2 = GameObject.FindWithTag("P2").GetComponent<Player2combat>();
        health2 = GameObject.FindWithTag("P2").GetComponent<Player2BiggeHealth>();
        currentcrunch = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCrunchUI();
        Blockcheck();

        if (attacking)
        {
            movement.movespeed = 6f;
            animator.SetBool("IsFlexing", false);
            flexing = false;
            flextime = 0;
            movement.canjump = false;

            lastclickedtime += 1 * Time.deltaTime;

            if (lastclickedtime >= maxcombodelay)
            {
                lastclickedtime = 0;
                Debug.Log("Attack Timeout");
                zpresses = 0;
                zp = 0;
                invs = false;
                attacking = false;
            }
        }
        else
        {

            movement.canjump = true;
        }

        if (comboend)
        {
            zpresses = 0;
            zp = 0;

            delaytimer += 1 * Time.deltaTime;

            if (delaytimer >= delay)
            {
                comboend = false;
                Debug.Log("attack Delay");
                delaytimer = 0;
            }
        }

        

        currentcrunch = Mathf.Clamp(currentcrunch, 0, maxcrunch);

        if (flexing)
        {
            flextime += 1 * Time.deltaTime;

            animator.SetBool("IsFlexing", true);

            if (flextime >= flexlength)
            {
                animator.SetBool("IsFlexing", false);
                flexing = false;
                flextime = 0;
            }
        }
        else
        {
            return;
        }

        if (movement.flipped)
        {
            knockbackx *= -1;
        }

        if (movement.hors == -1)
        {
            blockready = true;
        }
        else
        {
            blockready = false;
        }

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

    }
    public void updatebar(float damage)
    {
        currentcrunch += damage;
        Debug.Log("barupdate.");
    }
    public void UpdateCrunchUI()
    {

        float fillf = frontbar.fillAmount;
        float fillb = backbar.fillAmount;
        float hfrac = currentcrunch / maxcrunch;

        if (fillb > hfrac) //greatezr
        {
            frontbar.fillAmount = hfrac;
            backbar.color = Color.red;
            lerptimer += Time.deltaTime;
            float percentComplete = lerptimer / chipspeed;
            backbar.fillAmount = Mathf.Lerp(fillb, hfrac, percentComplete);
        }
        if (fillf < hfrac) //less
        {
            backbar.color = Color.green;
            backbar.fillAmount = hfrac;
            lerptimer += Time.deltaTime;
            float percentcomplete = lerptimer / chipspeed;
            frontbar.fillAmount = Mathf.Lerp(fillf, backbar.fillAmount, percentcomplete);
        }
    }

    public void Blockcheck()
    {
        if (Physics2D.OverlapBox(blockcheck.position, blockarea, 0, enemylayers))
        {
            blockpriming = true;

            if (blockready && blockpriming && combat2.attacking && movement.iscrouching == false)
            {
                isblocking = true;
                Debug.Log("GetBlockedBozo");
                animator.SetBool("IsBlocking", true);

            }
            else if (blockready && blockpriming && combat2.attacking && movement.iscrouching)
            {
                isblocking = true;
                Debug.Log("GetCBlockedBozo");
                animator.SetBool("IsCrouchBlocking", true);
            }
            else
            {
                isblocking = false;
                animator.SetBool("IsCrouchBlocking", false);
                animator.SetBool("IsBlocking", false);
            }
    
        }
        else
        {
            blockpriming = false;
        }
       
    }

    public void OnLightCrunch(InputAction.CallbackContext context)
    {
        if (currentcrunch > 100)
        {
            if (context.performed)
            {
                FindObjectOfType<SoundManager>().Play("BigThuddy3");
                currentcrunch -= 100;
                flexing = true;
                invs = true;
            }
        }
        else
        {
            return;
        }
    }
    public void OnLightKick(InputAction.CallbackContext context)
    {

        if (context.performed)
        {

            if (comboend == false)
            {
                if (canattack == true)
                {
                    zpresses++;
                    zp++;

                    if (movement.isjumping == true)
                    {
                        FindObjectOfType<SoundManager>().Play("Whoosh1");
                        attacking = true;
                        Debug.Log("AK 1");
                        animator.SetTrigger("AK1");
                        knockbackx = 40f;
                        knockbacky = 50f;
                        comboend = true;
                        zpresses = 0;
                        zp = 0;
                        delaytimer = 1f;
                    }
                    if (movement.isjumping == false)
                    {
                        if (zpresses == 1)
                        {
                            attacking = true;
                            Debug.Log("Kack 1");
                            animator.SetTrigger("IsLKicking");
                            knockbackx = -20f;
                            knockbacky = 10f;
                            comboend = false;
                        }

                        if (zpresses == 2)
                        {
                            attacking = true;
                            Debug.Log("Kack 2");
                            animator.SetTrigger("LK2");
                            knockbackx = -10f;
                            knockbacky = 20f;
                        }

                        if (zpresses == 3)
                        {
                            attacking = true;
                            Debug.Log("Kack 3");
                            animator.SetTrigger("LK3");
                            comboend = true;
                            knockbackx = 30f;
                            knockbacky = 50f;
                            lastclickedtime = .9f;
                            delaytimer = .6f;
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
                attacking = false;
            }
        }
    }
    public void OnLightPunch(InputAction.CallbackContext context)
    {

        if (context.performed)
        {

            if (comboend == false)
            {
                if (canattack == true)
                {
                    if (movement.isjumping == true)
                    {
                        FindObjectOfType<SoundManager>().Play("Whoosh1");
                        attacking = true;
                        Debug.Log("AK 1");
                        animator.SetTrigger("AK1");
                        knockbackx = 40f;
                        knockbacky = 50f;
                        comboend = true;
                        zpresses = 0;
                        zp = 0;
                        delaytimer = 1f;
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
                            attacking = true;
                            Debug.Log("Pawnch 3");
                            animator.SetTrigger("LP3");
                            comboend = true;
                            knockbackx = 40f;
                            knockbacky = 50f;
                            lastclickedtime = .9f;
                            delaytimer = .6f;
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

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "P2")
        {
            if (combat2.isblocking == false && combat2.invs == false)
            {
                if (attacking && combat2.attacking == false)
                {
                    Debug.Log("Inflicted Damage");
                    updatebar(attackDamage);
                    health2.takedamage(attackDamage);
                }

                if (flexing == false && attacking && combat2.attacking == true)
                {
                    rb.AddForce(new Vector2(knockbackx, 0), ForceMode2D.Impulse);
                    health2.takedamage(0);
                    FindObjectOfType<SoundManager>().Play("BigThuddy2");
                    Debug.Log("clash");
                }
                else if (flexing == true && attacking && combat2.attacking == true) //anti flex clash
                {
                    Debug.Log("Inflicted Damage");
                    updatebar(attackDamage);
                    health2.takedamage(attackDamage);
                    rb.AddForce(new Vector2(knockbackx, knockbacky), ForceMode2D.Impulse);
                }

                if (combat2.attacking && flexing)
                {
                    animator.SetBool("IsFlexing", false);
                    animator.SetTrigger("FlexAttack");
                    combat2.comboend = true;
                    health2.takedamage(attackDamage);
                    rb.AddForce(new Vector2(50f, 10f), ForceMode2D.Impulse);
                    attacking = true;
                }
            }
            else if (combat2.invs == true)
            {
                Debug.Log("whiff");
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

