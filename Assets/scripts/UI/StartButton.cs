using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public delegate void StartButtonClick();
    public static event StartButtonClick onClicked;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        Debug.Log("Clicked!");
        onClicked?.Invoke();
    }
}
