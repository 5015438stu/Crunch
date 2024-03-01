using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class parkchanger : MonoBehaviour
{
    public Image stageframe;
    public Sprite parkimage;
    public TextMeshProUGUI _title;
    public float map;
    public void ImageChangePark()
    {
        stageframe.sprite = parkimage;
        _title.text = "Park";
        map = 1;
    }
}
