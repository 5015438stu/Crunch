using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public int selectedcharacter = 0;
    public int selectedcharacter2 = 0;
    public potraitbiggee biggee;
    public potraitcinder cinder;
    public potraitjean jean;
    public eventsystem game;
    public int p1selected = 0;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<eventsystem>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<eventsystem>();

        p1selected = jean.selected;
        //p1selected = ash.selected;
        p1selected = cinder.selected;
        p1selected = biggee.selected;

        if (jean.selectchara == 1)
        {
            selectedcharacter = 1;
        }

        if (cinder.selectchara == 3)
        {
            selectedcharacter = 3;
        }

        if (biggee.selectchara == 4)
        {
            selectedcharacter = 4;
        }

        /// Player two

        if (jean.selectchara == 1)
        {
            if (p1selected == 2)
            {
                selectedcharacter2 = 1;
            }
        }

        if (cinder.selectchara == 3)
        {
            if (p1selected == 2)
            {
                selectedcharacter2 = 3;
            }
        }

        if (biggee.selectchara == 4)
        {
            if (p1selected == 2)
            {
                selectedcharacter2 = 4;
            }
        }
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
