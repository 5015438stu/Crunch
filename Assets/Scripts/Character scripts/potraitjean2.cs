using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class potraitjean2 : MonoBehaviour
{
    public Image oldimage;
    public Sprite newimage;
    public TextMeshProUGUI _title;
    public CharacterSelect select;
    public int selectchara = 0;
    public int selected = 0;

    public void ImageChange()
    {
        selectchara = 1;
        oldimage.sprite = newimage;
        _title.text = "Jean";
        selected = 1;
    }
}
