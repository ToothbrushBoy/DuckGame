using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class VolumeSettings : MonoBehaviour
{
    // Start is called before the first frame update
    public static float musicVolume;
    public static float effectVolume;
    
    public void setMusicVolume()
    {
        musicVolume = GetComponent<Slider>().value;
    }

    public void setEffectVolume()
    {
        effectVolume = GetComponent<Slider>().value;
    }
}
