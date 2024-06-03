using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausemenu : MonoBehaviour
{
    public static bool gamepaused = false;
    public int menuswtich = 0;

    public GameObject pausemenuUI;
    public GameObject charmenuUI;
    public GameObject Biggee;
    public GameObject Cinder;
    public GameObject Ash;
    public GameObject Jean;

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
    public void CharacterSelect()
    {
        charmenuUI.SetActive(true);
    }
    public void Characterexit()
    {
        charmenuUI.SetActive(false);
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
    public void Jeanspawn()
    {
        Jean.SetActive(true);
        Biggee.SetActive(false);
        Ash.SetActive(false);
        Cinder.SetActive(false);

    }

    public void Ashspawn()
    {
        Jean.SetActive(false);
        Biggee.SetActive(false);
        Ash.SetActive(true);
        Cinder.SetActive(false);

    }

    public void Cinderspawn()
    {
        Jean.SetActive(false);
        Biggee.SetActive(false);
        Ash.SetActive(false);
        Cinder.SetActive(true);

    }

    public void Biggeespawn()
    {
        Jean.SetActive(false);
        Biggee.SetActive(true);
        Ash.SetActive(false);
        Cinder.SetActive(false);

    }
}
