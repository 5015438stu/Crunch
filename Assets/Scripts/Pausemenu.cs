using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausemenu : MonoBehaviour
{
    public static bool gamepaused = false;
    public int menuswtich = 0;

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
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void pause()
    {
        pausemenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamepaused = true; 
    }
    public void Options()
    {
        menuswtich = 1;
    }
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}
