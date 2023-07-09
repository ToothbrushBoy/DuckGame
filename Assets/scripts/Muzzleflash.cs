using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzleflash : MonoBehaviour
{
    public float length;

    private Light flash;
    private LensFlare flare;
    private float timer = 0.15f;

    void Start()
    {
        flash = GetComponent<Light>();
        flare = GetComponent<LensFlare>();
    }

    void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                flash.enabled = false;
                flare.enabled = false;
                timer = 0f;
            }
        }
    }

    public void Play()
    {
        timer = length;
        flash.enabled = true;
        flare.enabled = true;
    }

}
