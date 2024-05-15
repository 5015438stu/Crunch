using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player2BiggeHealth : MonoBehaviour
{
    [Header("Health")]
    public float playerhealth = 1000;
    public float currenthealth;
    public float lerptimer;
    public float chipspeed = 2f;
    public bool hurt;
    public float stuntime = .5f;
    public float hurttime;
    public int lives = 2;
    public bool isdowned = false;
    public bool isdead = false;
    bool setdeath = false;

    [Header("Refs")]
    public Rigidbody2D rb;
    public PlayerCombat combat;
    public Player2combat combat2;
    public Player2Movement movement2;
    public Animator animator;
    public ParticleSystem hit = default;
    public GameObject pfp;
    public Image frontbar;
    public Image backbar;
    public BoxCollider2D hurtbox;
    public InputHandler inputHandler;

    [Header("Misc")]
    public GameObject[] score;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<GameObject>();
        GetComponent<Rigidbody2D>();
        GetComponent<Player2Movement>();
        combat = GameObject.FindWithTag("P1").GetComponent<PlayerCombat>();
        inputHandler = gameObject.AddComponent<InputHandler>();

        pfp.SetActive(true);
        currenthealth = playerhealth;
    }
    // Update is called once per frame
    void Update()
    {
        if (lives == 0)
        {
            isdead = true;
        }
        if (frontbar == null)
        {
            return;
        }
        if (backbar == null)
        {
            return;
        }
        currenthealth = Mathf.Clamp(currenthealth, 0, playerhealth);

        UpdateHealthUI();

        if (isdead)
        {
            inputHandler.p2death();
        }
        if (currenthealth <= 0)
        {
            movement2.canjump = false;
            movement2.movespeed = 0f;
            Die();
            isdowned = true;
            hurt = false;
            animator.SetBool("Hurt", false);
            animator.SetTrigger("KD2");

            if (setdeath == false)
            {
                lives -= 1;
                setdeath = true;
                FindObjectOfType<SoundManager>().Play("Dead1");
            }
            else
            {
                return;
            }

        }
        else
        {
            movement2.canjump = true;
            isdowned = false;
        }

        if (hurt)
        {
            if (combat.knockbacky > 40)
            {

                animator.SetBool("Hurt", false);
                animator.SetTrigger("KD1");
            }

            hurttime += 1 * Time.deltaTime;

            if (hurttime > stuntime)
            {
                hurt = false;
                hurttime = 0;
                animator.SetBool("Hurt", false);
            }
        }
     
    }

    public void takedamage(float damage)
    {
        if (isdead == false)
        {
            currenthealth -= damage;
            rb.AddForce(new Vector2(combat.knockbackx, combat.knockbacky), ForceMode2D.Impulse);
            lerptimer = 0f;
            animator.SetBool("Hurt", true);
            hurt = true;
            FindObjectOfType<SoundManager>().Play("Hurt1");
            hit.Play();
        }
        else
        {
            hurt = false;
            animator.SetBool("Hurt", false);
            return;
        }

        
    }
    
    public void restorehealth(int healamount)
    {
        currenthealth += healamount;
        lerptimer = 0f;
    }
    public void UpdateHealthUI()
    {
        float fillf = frontbar.fillAmount;
        float fillb = backbar.fillAmount;
        float hfrac = currenthealth / playerhealth;

        if (fillb > hfrac)
        {
            frontbar.fillAmount = hfrac;
            backbar.color = Color.red;
            lerptimer += Time.deltaTime;
            float percentComplete = lerptimer / chipspeed;
            backbar.fillAmount = Mathf.Lerp(fillb, hfrac, percentComplete);
        }
        if (fillf < hfrac)
        {
            backbar.color = Color.green;
            backbar.fillAmount = hfrac;
            lerptimer += Time.deltaTime;
            float percentcomplete = lerptimer / chipspeed;
            frontbar.fillAmount = Mathf.Lerp(fillf, backbar.fillAmount, percentcomplete);
        }
    }
    void Die()
    {

        if (isdead == false)
        {
            StartCoroutine(RoundChange());
        }
        else if (isdead == true)
        {
            inputHandler.p2death();
            Debug.Log("p2Death");
        }

    }


    IEnumerator RoundChange()
    {
        yield return new WaitForSeconds(3);
        score[lives].SetActive(true);
        currenthealth = playerhealth;
        setdeath = false;
        Debug.Log("Round Change");
        StopAllCoroutines();
    }

    
}
