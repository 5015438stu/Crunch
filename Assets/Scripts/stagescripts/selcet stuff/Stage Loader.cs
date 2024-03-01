using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageLoader : MonoBehaviour
{
    public float pkmap;
    public float plmap;

    public MapManager mapman;
    public parkchanger pkchanger;
    public plainschanger plchanger;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MapManager>();
        GetComponent<parkchanger>();
    }

    // Update is called once per frame
    void Update()
    {

        pkmap = pkchanger.map;

        plmap = plchanger.map;
    }
 
}
