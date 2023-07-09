using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    // Start is called before the first frame update
    public static float musicVolume = 0.5f;
    public static float effectVolume = 0.5f;

    void Awake()
    {

    }
        
    

    public void setMusicVolume()
    {
        musicVolume = GetComponent<Slider>().value;
    }

    public void setEffectVolume()
    {
        effectVolume = GetComponent<Slider>().value;
    }
}
