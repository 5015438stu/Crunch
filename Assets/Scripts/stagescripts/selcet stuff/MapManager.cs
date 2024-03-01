using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public float plmap;
    public float pkmap;

    public StageLoader loader;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<StageLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        plmap = loader.plmap;
        pkmap = loader.pkmap;
    }
    public void StageChange()
    {
        if (plmap == 1)
        {
            SceneManager.LoadScene("Plains");
        }
        if (pkmap == 2)
        {
            SceneManager.LoadScene("Park");
        }
    }
}
