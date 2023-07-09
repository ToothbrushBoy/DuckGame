using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolume : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(AudioSource audio in GetComponents<AudioSource>())
        {
            audio.volume = VolumeSettings.effectVolume;
        }
        
    }
}
