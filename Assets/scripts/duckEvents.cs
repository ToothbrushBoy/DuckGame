using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duckEvents : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Finish")
        {
            levelComplete();
        }else if(collision.gameObject.tag == "Bullet")
        {
            die();
        }
    }

    void die()
    {
        Debug.Log("died");
    }

    void levelComplete()
    {
        Debug.Log("level fin");
    }
}
