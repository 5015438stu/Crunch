using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plainstheme : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<SoundManager>().Play("PlainsTheme");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
