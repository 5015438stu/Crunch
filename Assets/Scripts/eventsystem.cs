using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventsystem : MonoBehaviour
{
    public CharacterSelect CS;
    public int p1chara;

    public GameObject P1Spawner;
    public GameObject p1spawn;
    public GameObject Biggee;
    public GameObject cinder;
    public GameObject ash;
    public GameObject Jean;

    bool p1spawned = false;

    // Start is called before the first frame update
    void Start()
    {
        CS = GameObject.FindWithTag("selector").GetComponent<CharacterSelect>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<CharacterSelect>();

        p1chara = CS.selectedcharacter;

        if (p1chara == 1)
        {
            p1chara = 69;
            spawnjean();
        }
        if (p1chara == 4)
        {
            spawnbiggee();
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
}
