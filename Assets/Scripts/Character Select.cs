using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public int selectedcharacter = 0;
    public potraitbiggee biggee;
    public potraitcinder cinder;
    public potraitjean jean;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (jean.selectchara == 1)
        {
            selectedcharacter = 1;
        }
        //if (Ash.selectchara == 2)
        //{
            //selectedcharacter = 2;
        //}
        if (cinder.selectchara == 3)
        {
            selectedcharacter = 3;
        }
        if (biggee.selectchara == 4)
        {
            selectedcharacter = 4;
        }
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
