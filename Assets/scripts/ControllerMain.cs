using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMain : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 duckSpawn;
    public GameObject duck;
    public GameObject farmer;
    public Canvas UI;
    private Canvas UiInstance;
    private GameObject duckInstance;
    private int score;

    void Start()
    {
        UiInstance = Instantiate(UI);
        StartButton.onClicked += test;
        duckEvents.death += playerDied;
        duckEvents.levelComplete += levelComplete;
    }

    private void levelComplete()
    {
        Debug.Log("Worked win");
    }

    private void playerDied()
    {
        Debug.Log("Worked died");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void test()
    {
        Debug.Log("Worked");
        UiInstance.gameObject.SetActive(false);
        duckInstance = Instantiate(duck, duckSpawn, Quaternion.identity);
        GameObject farmerInstance = Instantiate(farmer, duckSpawn + new Vector2(1, 0), Quaternion.identity);
    }
}
