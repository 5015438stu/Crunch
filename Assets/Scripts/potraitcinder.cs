using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class potraitcinder : MonoBehaviour
{
    public Image oldimage;
    public Sprite newimage;

    public void ImageChange()
    {
        oldimage.sprite = newimage;
    }
}
