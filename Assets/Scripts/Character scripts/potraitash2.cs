using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI;

public class potraitash2 : MonoBehaviour
{
    public Image oldimage;
    public Sprite newimage;
    public TextMeshProUGUI _title;
    public CharacterSelect select;
    public int selectchara = 0;
    public int selected = 0;

    public void ImageChange()
    {
        selectchara = 2;
        oldimage.sprite = newimage;
        _title.text = "Ash";
        selected = 1;
    }
}
