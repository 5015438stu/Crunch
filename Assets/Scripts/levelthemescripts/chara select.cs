using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charaselect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<SoundManager>().Play("CharacterSelectTheme");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void buttonsfx()
    {
        FindObjectOfType<SoundManager>().Play("ButtonSFX1");
    }
}
