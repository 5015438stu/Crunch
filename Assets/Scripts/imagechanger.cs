using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class imagechanger : MonoBehaviour
{
    public Image oldimage;
    public Sprite newimage;
    public CharacterSelect select;
    public int selectchara = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ImageChange()
    {
        oldimage.sprite = newimage;
    }
}
