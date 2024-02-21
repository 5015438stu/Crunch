using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AshCombat : MonoBehaviour
{

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
                lastclickedtime = 0;
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

        }

        //Add blocking and air attacks
    }
    public void lightpunch1()
    {
        attacking = true;
        Debug.Log("Pawnch 1");
        animator.SetTrigger("Lp1");
        knockbackx = -2f;
        knockbacky = 2f;
    }
    public void lightpunch2()
    {
        attacking = true;
        Debug.Log("Pawnch 2");
        animator.SetTrigger("Lp2");
        knockbackx = -1f;
        knockbacky = 3f;
    }
    public void lightpunch3()
    {
        attacking = true;
        Debug.Log("Pawnch 3");
        animator.SetTrigger("Lp3");
        comboend = true;
        zpresses = 0;
        zp = 0;
        knockbackx = 5f;
        knockbacky = 12f;
        lastclickedtime = +0.8f;
    }

}

