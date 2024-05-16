using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player2combat : MonoBehaviour
{
    [Header("Refs")]
    public HealthScript health;
    public Player2BiggeHealth health2;
    public Rigidbody2D rb;
    public Player2combat combat2;
    public PlayerCombat combat;
    public Player2Movement movement2;
    public Animator animator;
    public BoxCollider2D hitbox;
    public LayerMask enemylayers;
    public Image frontbar;
    public Image backbar;
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
    public bool invs = false;

    [Header("Knockback")]
    public float knockbackx = 0f; //Change later for each attack
    public float knockbacky = 0f; //Change later for each attack

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
        movement2 = GetComponent<Player2Movement>();
        combat = GameObject.FindWithTag("P1").GetComponent<PlayerCombat>();
        health = GameObject.FindWithTag("P1").GetComponent<HealthScript>();
        currentcrunch = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (comboend)
        {
            rb.constraints = RigidbodyConstraints2D.None;

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
            animator.SetBool("IsFlexing", false);
            flexing = false;
            flextime = 0;

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


        Blockcheck();
        UpdateCrunchUI();

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

        if (attacking == false)
        {
            knockbackx = 0;
            knockbacky = 0;
        }

        if (movement2.flipped)
        {
            knockbackx *= -1;
        }
        else
        {
            return;
        }

        if (movement2.hors <= .8)
        {
            blockready = false;
            
        }
        else
        {
            blockready = true;
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

            if (blockready && blockpriming && combat.attacking && movement2.iscrouching == false) /// blocking
            {
                isblocking = true;
                Debug.Log("GetBlockedBozo");
                animator.SetBool("IsBlocking", true);
            }
            else if(blockready && blockpriming && combat.attacking && movement2.iscrouching)
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
        if (context.performed)
        {
            flexing = true;
            invs = true;
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
                    FindObjectOfType<SoundManager>().Play("Whoosh1");
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
                if (movement2.isjumping == true)
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
        if (collision.gameObject.tag == "P1")
        {
            if (combat.isblocking == false && combat.invs == false)
            {
                if (attacking && combat.attacking == false) //normal hit
                {
                    Debug.Log("Inflicted Damage2");
                    updatebar(attackDamage);
                    health.takedamage(attackDamage);
                    rb.AddForce(new Vector2(knockbackx, knockbacky), ForceMode2D.Impulse);
                }

                if (flexing == false && attacking && combat.attacking == true) //clashing
                {
                    rb.AddForce(new Vector2(knockbackx, 0), ForceMode2D.Impulse);
                    FindObjectOfType<SoundManager>().Play("BigThuddy2");
                    Debug.Log("clash2");
                }
                else if (flexing == true && attacking && combat.attacking == true) //counter
                {
                    Debug.Log("Inflicted Damage");
                    updatebar(attackDamage);
                    health.takedamage(attackDamage);
                    rb.AddForce(new Vector2(knockbackx, knockbacky), ForceMode2D.Impulse);
                }
                if (combat.attacking && flexing)
                {
                    animator.SetBool("IsFlexing", false);
                    animator.SetTrigger("FlexAttack");
                    combat.comboend = true;
                    health.takedamage(attackDamage);
                    rb.AddForce(new Vector2(50f, 10f), ForceMode2D.Impulse);
                    attacking = true;
                }
            }
            else if (combat.invs == true)
            {
                Debug.Log("whiff2");
            }
          
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(blockcheck.position, blockarea);
    }
}
