using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2combat : MonoBehaviour
{
    [Header("Refs")]
    public HealthScript health;
    public Rigidbody2D rb;
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
    public float knockbackx = 0f; //Change later for each attack
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

    public void OnLightPunch(InputAction.CallbackContext context)
    {
        if (context.performed)
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
                Debug.Log("punch Timeout");
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
