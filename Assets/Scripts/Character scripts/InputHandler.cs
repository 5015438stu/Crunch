using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Cinemachine;

public class InputHandler : MonoBehaviour
{
    [Header("Refs")]
    static public InputHandler Instance;

    [SerializeField] TextMeshProUGUI Countdown;

    public CinemachineVirtualCamera vc;
    public CinemachineTargetGroup tg;
    public GameObject P1;
    public GameObject P2;
    public GameObject readytxt;
    public GameObject gotxt;

    [Header("Rounds")]
    public bool roundwon;
    public bool p1winner;
    public bool p2winner;
    public GameObject winscreen;
    public TextMeshProUGUI wintext;

    [Header("Countdown")]
    public float currenttime = 0f;
    public float startingtime = 300f;
    public bool count;

    [Header("Camera")]
    public bool isset = false;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        if (readytxt != null)
        {

            StartCoroutine(gamestart());
        }
        else
        {
            readytxt = null;
            return;
        }


        currenttime = startingtime;
        count = false;

        P1 = GameObject.FindWithTag("P1");
        P2 = GameObject.FindWithTag("P2");
    }

    void Update()
    {   
        if (roundwon)
        {
            winscreen.SetActive(true);

            if (p1winner)
            {
                wintext.text = "PLAYER 1 WON";
            }
            else if (p2winner)
            {
                wintext.text = "PLAYER 2 WON";
            }
        }
        if (gotxt == null)
        {
            return;
        }
        if (count)
        {
            currenttime -= 1 * Time.deltaTime;
            Countdown.text = currenttime.ToString("000");

            if (currenttime <= 0)
            {
                currenttime = 0;
            }
        }

        if (P1 != null)
        {
            SetCamera();

        } 
        else
        {
            P1 = GameObject.FindWithTag("P1");
        }

        if (P2 == null)
        {
            P2 = GameObject.FindWithTag("P2");
        } 
        else
        {
            return;
        }
    }
    public void p1death()
    {
        Debug.Log("roundover");
        roundwon = true;
        p2winner = true;
        count = false;
    }
    public void p2death()
    {
        Debug.Log("roundover");
        roundwon = true;
        p1winner = true;
        count = false;
    }
    public void SetCamera()
    {
        if (isset == false) 
        {
            isset = true;
            Debug.Log("CameraSet");
            tg.AddMember(P1.transform, 1, 0);
            tg.AddMember(P2.transform, 1, 0);
        } else
        {
            return;
        }
    }
    IEnumerator gamestart()
    {
        readytxt.SetActive(true);
        yield return new WaitForSeconds(1);
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
