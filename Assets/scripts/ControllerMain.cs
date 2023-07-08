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
    public GameObject farmerShotgun;
    public GameObject duckSpawnPoint;
    public Canvas UI;
    private Canvas UiInstance;
    private GameObject duckInstance;
    private int score;
    private TextMeshProUGUI scoreText;
    private GameObject menu;
    private GameObject gameUI;
    private ArrayList farmers;

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
        farmers = new ArrayList();
    }

    private void levelComplete()
    {
        score++;
        scoreText.text = score + "";
        duckInstance.transform.position = duckSpawn;

    }

    private void playerDied()
    {
        score = 0;
        menu.SetActive(true);
        gameUI.SetActive(false);
        Destroy(duckInstance);
        foreach(GameObject farmerInstance in farmers)
        {
            Destroy(farmerInstance);
        }
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
        farmers.Add(Instantiate(farmer, duckSpawn + new Vector3(2, 0, 0), Quaternion.identity));
        farmers.Add(Instantiate(farmerShotgun, duckSpawn + new Vector3(4, 0, 0), Quaternion.identity));
        farmers.Add(Instantiate(farmer, duckSpawn + new Vector3(6, 0, 0), Quaternion.identity));
    }
}
