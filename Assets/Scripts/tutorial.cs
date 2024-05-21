using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial : MonoBehaviour
{
    public GameObject tut;

    void Start()
    {
        Time.timeScale = 0f;
    }
    public void exittut()
    {
        Time.timeScale = 1f;
        tut.SetActive(false);
    }
}
