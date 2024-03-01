using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class plainschanger : MonoBehaviour
{
    public Image stageframe;
    public Sprite plainsimage;
    public TextMeshProUGUI _title;
    public float map;
    public void ImageChangePlains()
    {
        stageframe.sprite = plainsimage;
        _title.text = "Plains";
        map = 1;
    }
}
