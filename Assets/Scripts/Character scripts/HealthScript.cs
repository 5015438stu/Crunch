using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public float playerhealth = 1000;
    public float currenthealth;
    public float lerptimer;
    public float chipspeed = 2f;
    public Image frontbar;
    public Image backbar;

    public Rigidbody2D rb;
    Animator animator;
    public PlayerCombat combat;
    public Player2combat combat2;
    public GameObject pfp;

    // Start is called before the first frame update
    void Start()
    {
        pfp.SetActive(true);
        currenthealth = playerhealth;
        GetComponent<GameObject>();
        GetComponent<Rigidbody2D>();
        GetComponent<Player2combat>();
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
    }
    public void takedamage(float damage)
    {
        currenthealth -= damage;
        lerptimer = 0f;
        animator.SetBool("Hurt", true);

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
        Debug.Log("LETMEGETUP");
        // die animation
        // respawn dummy
    }
}
