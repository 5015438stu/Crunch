using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausemenu : MonoBehaviour
{
    public static bool gamepaused = false;

    public GameObject pausemenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamepaused)
            {
                resume();
            } else
            {
                pause();
            }
        }
    }

    public void resume()
    {
        pausemenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamepaused = false;
    }

    void pause()
    {
        pausemenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamepaused = true; 
    }
    public void loadmenu()
    {

    }
    public void Options()
    {

    }
    public void quitgame()
    {
        Application.Quit();
    }
}
