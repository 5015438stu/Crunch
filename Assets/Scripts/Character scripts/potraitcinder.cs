using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class potraitcinder : MonoBehaviour
{
    public Image oldimage;
    public Sprite newimage;
    public CharacterSelect select;
    public TextMeshProUGUI _title;
    public int selectchara = 0;
    public int selected = 0;
    public void ImageChange()
    {
        selectchara = 3;
        oldimage.sprite = newimage;
        _title.text = "Biggee";
        selected = 1;
}
}
