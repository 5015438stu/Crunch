using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Cinemachine;

public class InputHandler : MonoBehaviour
{
    static public InputHandler Instance;

    float currenttime = 0f;
    public float startingtime = 300f;
    public CinemachineVirtualCamera vc;
    public CinemachineTargetGroup tg;
    public GameObject readytxt;
    public GameObject gotxt;
    public bool count;
    public GameObject P1;
    public GameObject P2;
    public float roundcount;
    public float rountwincount;

    [SerializeField] TextMeshProUGUI Countdown;

    void Start()
    {
        SetCamera();
        StartCoroutine(gamestart());
        currenttime = startingtime;
        count = false;
        P1 = GameObject.Find("Biggee(Clone)");
        P2 = GameObject.Find("P2 Biggee(Clone)");
        vc = gameObject.GetComponent<CinemachineVirtualCamera>();
        tg = gameObject.GetComponent<CinemachineTargetGroup>();
    }

    void Update()
    {
         
        if (count)
        {
            currenttime -= 1 * Time.deltaTime;
            Countdown.text = currenttime.ToString("0");

            if (currenttime <= 0)
            {
                currenttime = 0;
            }
        }
        
    }
    public void SetCamera()
    {
        tg.AddMember(P1.transform, 1, 0);
    }
    IEnumerator gamestart()
    {
        /*rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionY;*/
        readytxt.SetActive(true);
        yield return new WaitForSeconds(1);
        /*rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.None;
        rb.AddForce(new Vector2(Xspeed, Yspeed * -5f), ForceMode2D.Impulse);*/
        readytxt.SetActive(false);
        gotxt.SetActive(true);
        StartCoroutine(textOff());
    }
    IEnumerator textOff()
    {
        yield return new WaitForSeconds(1);
        count = true;
        gotxt.SetActive(false);
    }
}
