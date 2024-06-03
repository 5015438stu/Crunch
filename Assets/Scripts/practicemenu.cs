using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class practicemenu : MonoBehaviour
{
    public GameObject pause;
    public GameObject options;

    public Pausemenu PM;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Pausemenu>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void switchmenu()
    {
        pause.SetActive(false);
        options.SetActive(true);
    }

    
}
