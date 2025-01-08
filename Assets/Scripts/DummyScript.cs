using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class DummyScript : MonoBehaviour
{
    public int maxhealth = 1000;
    public int currenthealth;
    bool hit = false;
    public float hittime = 0.3f;
    float hitstun;
    public float speed;
    public bool knockdown = false;

    public Animator animator;
    public BoxCollider2D hitbox;
    Rigidbody2D rb;
    public PlayerCombat combat;
    public AshCombat ashcombat;
    public Life healtbar;
    public Dummyhealth health;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currenthealth = maxhealth;
    }
     void Update()
    {
        health.SetHealth(currenthealth); //replace
        if (rb != null)
        {
            speed = rb.linearVelocity.magnitude;
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
        if (collision.gameObject.tag == "Ground")
        {
            animator.SetTrigger("KD2");
        }
        if (collision.gameObject.tag == "P1")
        {
            if (combat.attacking == true)
            {
                if (hit == false)
                {
                    currenthealth -= combat.attackDamage;
                    health.takedamage(combat.attackDamage);
                    currenthealth -= ashcombat.attackDamage;
                    health.takedamage(ashcombat.attackDamage);

                    animator.SetBool("Hurt", true);
                    hit = true;

                    rb.AddForce(new Vector2(combat.knockbackx, combat.knockbacky), ForceMode2D.Impulse);



                }
                if (combat.comboend == true)
                {
                    animator.SetTrigger("KD1");
                    knockdown = true;
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