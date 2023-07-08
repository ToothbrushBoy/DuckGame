using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duckEvents : MonoBehaviour
{
    public delegate void Death();
    public static event Death death;

    public delegate void Hit();
    public static event Hit hit;

    public delegate void LevelComplete();
    public static event LevelComplete levelComplete;
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
            _hit();
        }
    }

    void _hit()
    {
        hit?.Invoke();

        die();
    }

    void die()
    {
        Debug.Log("died");
        death?.Invoke();
    }

    void levelfin()
    {
        Debug.Log("level fin");
        levelComplete?.Invoke();
    }
}
