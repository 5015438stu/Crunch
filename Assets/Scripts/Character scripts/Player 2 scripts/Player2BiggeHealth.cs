using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player2BiggeHealth : MonoBehaviour
{
    public float playerhealth = 1000;
    public float currenthealth;
    public float lerptimer;
    public float chipspeed = 2f;
    public bool hurt;
    public float stuntime = .5f;
    public float hurttime;

    public Rigidbody2D rb;
    public PlayerCombat combat;
    public Player2combat combat2;
    public Player2Movement movement;
    public Animator animator;
    public ParticleSystem hit = default;
    public GameObject pfp;
    public Image frontbar;
    public Image backbar;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<GameObject>();
        GetComponent<Rigidbody2D>();
        GetComponent<Player2Movement>();
        combat = GameObject.FindWithTag("P1").GetComponent<PlayerCombat>();

        pfp.SetActive(true);
        currenthealth = playerhealth;
    }
    // Update is called once per frame
    void Update()
    {
        currenthealth = Mathf.Clamp(currenthealth, 0, playerhealth);

        UpdateHealthUI();

        if (currenthealth <= 0)
        {
            Die();
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
        Debug.Log("LETMEGETUP");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
