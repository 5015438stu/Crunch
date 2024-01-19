using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public int playerhealth = 1000;
    int currenthealth;


    public PlayerCombat combat;
    // Start is called before the first frame update
    void Start()
    {
        currenthealth = playerhealth;
    }
    // Update is called once per frame
    public void takedamage(int damage)
    {

        if (currenthealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("LETMEGETUP");
        // die animation
        // respawn dummy
    }
}
