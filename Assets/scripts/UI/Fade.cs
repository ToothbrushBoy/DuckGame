using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        duckEvents.death += fadeToBlak;
    }

    private void fadeToBlak()
    {
        GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 100f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
