using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class returnToMenu : MonoBehaviour
{

    public delegate void ReturnToMenu();
    public static event ReturnToMenu OnClick;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void returnMenu()
    {
        OnClick?.Invoke();
    }
}
