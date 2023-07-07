using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControllerMain : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 duckSpawn;
    public GameObject duck;
    public GameObject farmer;
    public GameObject duckSpawnPoint;
    public Canvas UI;
    private Canvas UiInstance;
    private GameObject duckInstance;
    private int score;
    private TextMeshProUGUI scoreText;
    private GameObject menu;
    private GameObject gameUI;

    void Start()
    {
        duckSpawn = duckSpawnPoint.transform.position;
        UiInstance = Instantiate(UI);
        StartButton.onClicked += startGame;
        duckEvents.death += playerDied;
        duckEvents.levelComplete += levelComplete;
        menu = UiInstance.transform.GetChild(0).gameObject;
        gameUI = UiInstance.transform.GetChild(1).gameObject;
        scoreText = gameUI.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void levelComplete()
    {
        score++;
        scoreText.text = score + "";
        duckInstance.transform.position = duckSpawn;

    }

    private void playerDied()
    {
        Debug.Log("Worked died");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        menu.SetActive(false);
        gameUI.SetActive(true);
        duckInstance = Instantiate(duck, duckSpawn, Quaternion.identity);
        GameObject farmerInstance = Instantiate(farmer, duckSpawn + new Vector3(2, 0, 0), Quaternion.identity);
    }
}
