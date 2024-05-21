using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public GameObject options;
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Exit()
    {
        Application.Quit();
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
    public void Optionbutton()
    {
        options.SetActive(true);
    }
    public void CLoseptionbutton()
    {
        options.SetActive(false);
    }
    public void buttonsfx()
    {
        FindObjectOfType<SoundManager>().Play("ButtonSFX1");
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
