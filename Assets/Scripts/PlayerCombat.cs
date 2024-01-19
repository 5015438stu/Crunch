using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;

    public BoxCollider2D hitbox;

    public LayerMask enemylayers;
    public int attackDamage = 40; //Change later for each attack
    public float knockback = 0f; //Change later for each attack
    public float lastclickedtime = 0f;
    public float maxcombodelay = 1f;
    public static int zpresses = 0;
    public float attackwait;
    public bool attacking;
    public float zp = 0f;

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
                attacking = true;
                Debug.Log("Pawnch 1");
                animator.SetTrigger("LP1");
                knockback = -2f;
            }

            if (zpresses == 2)
            {
                attacking = true;
                Debug.Log("Pawnch 2");
                animator.SetTrigger("LP2");
                knockback = -1f;
            }

            if (zpresses == 3)
            {
                attacking = true;
                Debug.Log("Pawnch 3");
                animator.SetTrigger("LP3");
                zpresses = 0;
                zp = 0;
                knockback = 5f;
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
}

