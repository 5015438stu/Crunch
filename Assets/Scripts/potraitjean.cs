using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class potraitjean : MonoBehaviour
{
    public Image oldimage;
    public Sprite newimage;
    public CharacterSelect select;
    public int selectchara = 0;

    public void ImageChange()
    {
        selectchara = 1;
        oldimage.sprite = newimage;
    }
}
