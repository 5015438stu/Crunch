using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    [Header("Health")]
    public float playerhealth = 1000;
    public float currenthealth;
    public float lerptimer;
    public float chipspeed = 2f;
    public bool hurt;
    public float stuntime = .5f;
    public float hurttime;

    [Header("Refs")]
    public Rigidbody2D rb;
    public PlayerCombat combat;
    public Player2combat combat2;
    public PlayerMovement move;
    public Animator animator;
    public ParticleSystem hit = default;
    public GameObject pfp;
    public Image frontbar;
    public Image backbar;

    [Header("Misc")]
    public float deaths;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<GameObject>();
        GetComponent<PlayerMovement>();
        GetComponent<Rigidbody2D>();
        GetComponent<PlayerMovement>();
        combat2 = GameObject.FindWithTag("P2").GetComponent<Player2combat>();

        pfp.SetActive(true);
        currenthealth = playerhealth;
    }
    // Update is called once per frame
    void Update()
    {
        currenthealth = Mathf.Clamp(currenthealth, 0, playerhealth);

        if (frontbar != null )
        {
            UpdateHealthUI();
        }
        else
        {
            return;
        }

        if (currenthealth <= 0)
        {
            move.movespeed = 0f;
            Die();
        }
        else
        {
            move.movespeed = 8f;
        }

        if (hurt)
        {


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
        currenthealth -= damage;
        rb.AddForce(new Vector2(combat.knockbackx, combat.knockbacky), ForceMode2D.Impulse);
        lerptimer = 0f;
        animator.SetBool("Hurt", true);
        hurt = true;
        FindObjectOfType<SoundManager>().Play("Hurt1");
        hit.Play();

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
        if(fillf < hfrac)
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
        Debug.Log("LETP1GETUP");


        deaths++;

        if (InputHandler.Instance != null)
        {
            Debug.Log("Round Change");
            InputHandler.Instance.Roundchange();
        }
        else
        {
            return;
        }

        StartCoroutine(RoundChange());
    }

    IEnumerator RoundChange()
    {
        if (move.isjumping == true)
        {
            animator.SetTrigger("KD2");
        }
        else
        {
            animator.SetTrigger("KD1");
        }

        yield return new WaitForSeconds(3);


        currenthealth = playerhealth;

    }
}
