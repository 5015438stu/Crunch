using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class damagescript : MonoBehaviour
{
    public float hittime = 0.3f;
    float hitstun;
    bool hit = false;
    bool knockdown = false;
    public float speed;
    public int maxhealth = 1000;
    public int currenthealth;
    public float hittime = 0;

    public Animator animator;
    public BoxCollider2D hitbox;
    public Rigidbody2D rb;
    public PlayerCombat combat;
    public Life healtbar;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healtbar.SetMaxHealth(currenthealth);
    }

    // Update is called once per frame
    void Update()
    {
        healtbar.SetHealth(currenthealth);

        if (knockdown)
        {

        }
        if (hit)
        {
            hitstun += Time.deltaTime;

            if (hitstun > hittime) ///add each charas combat
            {
                hitstun = 0;
                animator.SetBool("Hurt", false);
                hit = false;
            }
        }
        if (currenthealth <= 0)
        {
            Die();
        }

        //add damage, knockback, and the knockdown state.

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

        }
    }
    void Die()
    {
        Debug.Log("LETEMGETUP");
        // die animation
        // respawn dummy
    }
}
