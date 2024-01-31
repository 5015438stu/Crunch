using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventsystem : MonoBehaviour
{
    public CharacterSelect CS;
    public int p1chara;
    public int p2chara;

    public GameObject p2spawn;
    public GameObject p1spawn;
    public GameObject Biggee;
    public GameObject cinder;
    public GameObject ash;
    public GameObject Jean;

    bool p1spawned = false;
    bool p2spawned = false;

    // Start is called before the first frame update
    void Start()
    {
        CS = GameObject.FindWithTag("selector").GetComponent<CharacterSelect>();

        p1chara = CS.selectedcharacter;
        p2chara = CS.selectedcharacter2;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<CharacterSelect>();

        if (p1chara == 1)
        {
            spawnjean();
            p1chara = 0;
        }
        if (p1chara == 4)
        {
            spawnbiggee();
            p1chara = 0;
        }

        if (p2chara == 1)
        {
            spawnjean2();
            p2chara = 0;
        }
        if (p2chara == 4)
        {
            spawnbiggee2();
            p2chara = 0;
        }
    }

    void spawnbiggee()
    {
        p1spawned = true;
        Instantiate(Biggee, new Vector3(p1spawn.transform.position.x, p1spawn.transform.position.y, p1spawn.transform.position.x), transform.rotation);
    }
    void spawnjean()
    {
        p1spawned = true;
        Instantiate(Jean, new Vector3(p1spawn.transform.position.x, p1spawn.transform.position.y, p1spawn.transform.position.x), transform.rotation);
    }

    void spawnbiggee2()
    {
        p1spawned = true;
        Instantiate(Biggee, new Vector3(p2spawn.transform.position.x, p1spawn.transform.position.y, p1spawn.transform.position.x), transform.rotation);
    }
    void spawnjean2()
    {
        p1spawned = true;
        Instantiate(Jean, new Vector3(p2spawn.transform.position.x, p1spawn.transform.position.y, p1spawn.transform.position.x), transform.rotation);
    }
}
