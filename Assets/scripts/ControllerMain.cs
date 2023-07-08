using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq;
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
    private GameObject fade;
    private ArrayList farmers;
    public GameObject spawnParent;
    private Transform[] farmerSpawns;

    void Start()
    {
        duckSpawn = duckSpawnPoint.transform.position;
        UiInstance = Instantiate(UI);
        StartButton.onClicked += startGame;
        duckEvents.death += playerDied;
        duckEvents.hit += duckFall;
        duckEvents.levelComplete += levelComplete;
        menu = UiInstance.transform.GetChild(0).gameObject;
        gameUI = UiInstance.transform.GetChild(1).gameObject;
        fade = UiInstance.transform.GetChild(2).gameObject;
        scoreText = gameUI.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        farmers = new ArrayList();
        getFarmerSpawnPoints();
    }

    private void duckFall()
    {
        Debug.Log("still brokey");
        disableFarmers();
    }

    private void levelComplete()
    {
        fade.SetActive(true);
        score++;
        killFarmers();
        StartCoroutine(startNext());

    }

    private IEnumerator startNext()
    {
        Debug.Log("waiting");
        
        yield return new WaitForSeconds(1f);
        duckInstance.transform.position = duckSpawn;
        scoreText.text = score + "";
        
        spawnFarmers(score + 1);
    }

    private void playerDied()
    {
        score = 0;
        menu.SetActive(true);
        gameUI.SetActive(false);
        Destroy(duckInstance);
        killFarmers();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void getFarmerSpawnPoints()
    {
        farmerSpawns = spawnParent.transform.GetComponentsInChildren<Transform>();
        Debug.Log(farmerSpawns.Length);
    }

    public void startGame()
    {
        duckEvents.hit -= duckFall;
        duckEvents.hit += duckFall;
        menu.SetActive(false);
        gameUI.SetActive(true);
        duckInstance = Instantiate(duck, duckSpawn, Quaternion.identity);
        spawnFarmers(score+1);
    }

    private void spawnFarmers(float difficulty)
    {
        ArrayList used = new ArrayList();
        farmers = new ArrayList();
        int place;
        float currentDiff = 0f;
        while(currentDiff < difficulty && used.Count < farmerSpawns.Length-1)
        {
            //this is 1 start of range becuse we want to ignore the first entry in the list
            place = UnityEngine.Random.Range(1, farmerSpawns.Length - 1);
            while (used.Contains(place))
            {
                place = UnityEngine.Random.Range(1, farmerSpawns.Length);
            }
            var spawn = farmerSpawns[place];
            used.Add(place);
            farmers.Add(Instantiate(farmer, spawn.position, Quaternion.identity));
            currentDiff += 1;
        }
    }

    private void killFarmers()
    {
        Debug.Log("marers :" + "s");
        foreach (GameObject farmerInstance in farmers)
        {
            Destroy(farmerInstance);
        }
    }

    private void disableFarmers()
    {
        foreach (GameObject farmerInstance in farmers)
        {
            var f = farmerInstance.GetComponent<Farmer>();
            if(f != null)
            {
                f.enabled = false;
            }

            var fs = farmerInstance.GetComponent<ShotgunFarmer>();
            if (fs != null)
            {
                fs.enabled = false;
            }

        }
    }

    private void enableFarmers()
    {
        foreach (GameObject farmerInstance in farmers)
        {
            var f = farmerInstance.GetComponent<Farmer>();
            if (f != null)
            {
                f.enabled = true;
            }

            var fs = farmerInstance.GetComponent<ShotgunFarmer>();
            if (fs != null)
            {
                fs.enabled = true;
            }

        }
    }
}
