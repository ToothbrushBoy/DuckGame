using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        duckEvents.levelComplete += levelComplete;
    }

    public void levelComplete()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
