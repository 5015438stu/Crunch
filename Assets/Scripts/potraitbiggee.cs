using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class potraitbiggee : MonoBehaviour
{
    public Image oldimage;
    public Sprite newimage;
    public Image oldimage2;
    public CharacterSelect select;
    public int selectchara = 0;
    public int selected = 0;
    public void ImageChange()
    {
        selectchara = 4;
        oldimage.sprite = newimage;
        selected = 1; 
    }
}
