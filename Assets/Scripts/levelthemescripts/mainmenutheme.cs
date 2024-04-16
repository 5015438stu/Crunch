using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainmenutheme : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<SoundManager>().Play("MainMenuTheme");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
