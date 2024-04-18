using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermanager : MonoBehaviour
{
    public GameObject readytxt;
    public GameObject gotxt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    /*IEnumerator gamestart()
    {
        *//*rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionY;*//*
        readytxt.SetActive(true);
        yield return new WaitForSeconds(1);
        *//*rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.None;
        rb.AddForce(new Vector2(Xspeed, Yspeed * -5f), ForceMode2D.Impulse);*//*
        readytxt.SetActive(false);
        gotxt.SetActive(true);
        StartCoroutine(textOff());
    }
    IEnumerator textOff()
    {
        yield return new WaitForSeconds(1);
        gotxt.SetActive(false);
    }*/
}
