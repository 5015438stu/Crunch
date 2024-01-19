using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class DummyScript : MonoBehaviour
{
    public int maxhealth = 1000;
    int currenthealth;
    bool hit = false;
    public float hittime = 0.3f;

    float hitstun;
    public float speed;

    public Animator animator;
    public BoxCollider2D hitbox;
    
    Rigidbody2D rb;
    public PlayerCombat combat;
    public Life healtbar;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currenthealth = maxhealth;
        healtbar.SetMaxHealth(currenthealth);
    }
     void Update()
    {
        healtbar.SetHealth(currenthealth);
        if (rb != null)
        {
            speed = rb.velocity.magnitude;
        }

        if (hit)
        {
            hitstun += Time.deltaTime;

            if (hitstun > hittime)
            {
                hitstun = 0;
                animator.SetBool("Hurt", false);
                hit = false;
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "P1")
        {
            if (combat.attacking == true)
            {
                if (hit == false)
                {
                    currenthealth -= combat.attackDamage;
                    animator.SetBool("Hurt", true);
                    hit = true;
                    rb.AddForce(new Vector2(combat.knockback, 0), ForceMode2D.Impulse);

                }

            }
           
            if (currenthealth <= 0)
            {
                Die();
            }
        }
    }
    void Die()
    {
        Debug.Log("LETEMGETUP");
        // die animation
        // respawn dummy
    }
}   