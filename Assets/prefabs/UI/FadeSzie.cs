using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSzie : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        var res = Screen.currentResolution;
        GetComponent<RectTransform>().sizeDelta = new Vector2(res.width, res.height);
    }
}
