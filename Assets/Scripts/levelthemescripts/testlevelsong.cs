using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testlevelsong : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<SoundManager>().Play("Stay inside me");
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
}
