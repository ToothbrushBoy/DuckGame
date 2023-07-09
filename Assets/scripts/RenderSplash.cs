using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RenderSplash : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        ScreenCapture.CaptureScreenshot("Splash-Small.png");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
