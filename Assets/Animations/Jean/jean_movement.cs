using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class jean_movement : MonoBehaviour
{
    public Animator animator;
    public float speed = 280;
    public float jumpforce = 20;
    public float jumpheight = 5;
    public float gravityScale = 5;
    public float fallgravityscale = 15;
    public float buttontime = 0.3f;
    public float cancelrate = 100;
    public float jumpamount = 1;

    Vector2 move;

    Rigidbody2D rb;

    bool jumping;
    float jumptime;
    bool jumpcancelled;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
