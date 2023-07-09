using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duckEvents : MonoBehaviour
{
    public delegate void Death();
    public static event Death death;

    public delegate void Hit();
    public static event Hit hit;
    public delegate void Fall();
    public static event Fall fall;

    private bool dead;

    public delegate void LevelComplete();
    public static event LevelComplete levelComplete;

    public AudioSource hitSfx;
    public AudioSource fallSfx;
    // Start is called before the first frame update
    void Start()
    {
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("there is a collison");
        if(collision.gameObject.tag == "Finish")
        {
            levelComplete();
        }else if(collision.gameObject.tag == "Bullet")
        {
            Debug.Log("been hit");
            StartCoroutine(hitCo());
        }else if(collision.gameObject.tag == "Ground" && dead)
        {
            Debug.Log("dead on ground");
            die();
        }
    }

    IEnumerator hitCo()
    {
        hit?.Invoke();
        fallSfx.Pause();
        hitSfx.Play();

        yield return new WaitForSeconds(1.5f);
        _fall();
    }

    void _fall()
    {
        fallSfx.Play();
        fall?.Invoke();
        dead = true;
        //death?.Invoke();
    }

    void die()
    {
        death?.Invoke();

    }

    void levelfin()
    {
        Debug.Log("level fin");
        levelComplete?.Invoke();
    }
}
