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
    private GameObject endScreen;
    private GameObject fade;
    private ArrayList farmers;
    public GameObject spawnParent;
    private Transform[] farmerSpawns;
    public delegate void Reset();
    public static event Reset reset;
    public List<GameObject> farmerTypes;
    //public delegate void FarmerTick();
    //public static event FarmerTick farmerTick;

    void Start()
    {
        duckSpawn = duckSpawnPoint.transform.position;
        UiInstance = Instantiate(UI);
        StartButton.onClicked += startGame;
        duckEvents.death += playerDied;
        duckEvents.hit += duckFall;
        duckEvents.levelComplete += levelComplete;
        returnToMenu.OnClick += returnToMainMenu;
        menu = UiInstance.transform.GetChild(0).gameObject;
        gameUI = UiInstance.transform.GetChild(1).gameObject;
        fade = UiInstance.transform.GetChild(2).gameObject;
        endScreen = UiInstance.transform.GetChild(3).gameObject;
        scoreText = gameUI.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        gameUI = UiInstance.transform.GetChild(1).gameObject;
        farmers = new ArrayList();

        //not working :/
        //StartCoroutine(farmer_tick());

        getFarmerSpawnPoints();
    }

    /*private IEnumerator farmer_tick()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            farmerTick?.Invoke();
        }
    }*/

    private void duckFall()
    {
        Debug.Log("still brokey");
        disableFarmers();
    }

    private void levelComplete()
    {
        fade.SetActive(true);
        
        StartCoroutine(startNext());

    }

    private IEnumerator startNext()
    {
        Debug.Log("waiting");
        score++;
        duck.GetComponent<CircleCollider2D>().enabled = false;
        disableFarmers();
        yield return new WaitForSeconds(1f);
        killFarmers();
        reset?.Invoke();
        duckInstance.transform.position = duckSpawn;
        scoreText.text = score + "";
        spawnFarmers(score + 1);
        disableFarmers();
        yield return new WaitForSeconds(1f);
        enableFarmers();
        fade.SetActive(false);
        duck.GetComponent<CircleCollider2D>().enabled = true;
    }

    private void playerDied()
    {
        score = 0;
        menu.SetActive(false);
        gameUI.SetActive(false);
        endScreen.SetActive(true);
        Destroy(duckInstance);
        killFarmers();
        reset?.Invoke();


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
        endScreen.SetActive(false);
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

            int type = UnityEngine.Random.Range(0, farmerTypes.Count);
            var toSpawn = farmerTypes[type];
            //this is 1 start of range becuse we want to ignore the first entry in the list
            place = UnityEngine.Random.Range(1, farmerSpawns.Length - 1);




            while (used.Contains(place))
            {
                place = UnityEngine.Random.Range(1, farmerSpawns.Length);
            }
            var spawn = farmerSpawns[place];
            used.Add(place);
            var newFarmer = Instantiate(toSpawn, spawn.position, Quaternion.identity);


            newFarmer.GetComponent<Difficulty>().scaling = 1 + (difficulty-1) / 10;
            farmers.Add(newFarmer);
            currentDiff += toSpawn.GetComponent<Difficulty>().difficulty;
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

    public void returnToMainMenu()
    {
        score = 0;
        menu.SetActive(true);
        gameUI.SetActive(false);
        endScreen.SetActive(false);
        Destroy(duckInstance);
        killFarmers();
        reset?.Invoke();
    }

}
