using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parktheme : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<SoundManager>().Play("ParkTheme");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
