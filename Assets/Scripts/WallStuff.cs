using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallStuff : MonoBehaviour
{

    BoxCollider2D wall;
    Rigidbody2D rb;
    DummyScript dummy;

    // Start is called before the first frame update
    void Start()
    {
      wall = GetComponent<BoxCollider2D>();
      rb = GetComponent<Rigidbody2D>();
      dummy = GetComponent<DummyScript>();
    }

    // Update is called once per frame
    public void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
