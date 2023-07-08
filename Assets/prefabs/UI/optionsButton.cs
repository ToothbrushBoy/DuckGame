using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class optionsButton : MonoBehaviour
{
    public delegate void OptionsMenu();
    public static event OptionsMenu goToOptions;
    // Start is called before the first frame update

    public void Options()
    {
        goToOptions?.Invoke();
    }
}
