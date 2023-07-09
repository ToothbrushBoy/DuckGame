using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine;

public class ControllerMain : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 duckSpawn;
    public GameObject duck;
    public GameObject farmer;
    public GameObject farmerShotgun;
    public GameObject farmerSmg;
    public GameObject duckSpawnPoint;
    public Canvas UI;
    private Canvas UiInstance;
    private GameObject duckInstance;
    private int score;
    private float difficulty;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI scoreTextEnd;
    private GameObject menu;
    private GameObject gameUI;
    private GameObject endScreen;
    private GameObject fade;
    private GameObject optionsMenu;
    private ArrayList farmers;
    public GameObject spawnParent;
    private Transform[] farmerSpawns;
    public delegate void Reset();
    public static event Reset reset;
    public List<GameObject> farmerTypes;
    private Controls _controls;
    private InputAction pauseButton;
    private bool paused;
    public AudioSource music;
    private GameObject border;

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
        optionsButton.goToOptions += openOptionsMenu;
        StartButton.resetClicked += resetGame;
        menu = UiInstance.transform.GetChild(0).gameObject;
        gameUI = UiInstance.transform.GetChild(1).gameObject;
        fade = UiInstance.transform.GetChild(2).gameObject;
        endScreen = UiInstance.transform.GetChild(3).gameObject;
        scoreText = gameUI.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        scoreTextEnd = endScreen.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        gameUI = UiInstance.transform.GetChild(1).gameObject;
        optionsMenu = UiInstance.transform.GetChild(4).gameObject;
        farmers = new ArrayList();
        paused = false;
        border = GameObject.FindGameObjectsWithTag("Border")[0];

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

    private void Awake()
    {
        _controls = new Controls();
        pauseButton = _controls.duck.Pause;
        pauseButton.performed += PauseButton_performed;
    }
    private void PauseButton_performed(InputAction.CallbackContext obj)
    {
        if (paused)
        {
            Time.timeScale = 1;
            paused = !paused;
            gameUI.SetActive(true);
            endScreen.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            paused = !paused;
            gameUI.SetActive(false);
            endScreen.SetActive(true);
            scoreTextEnd.text = score + "";
        }
        
    }

    private void OnEnable()
    {
        
        
    }

    private void duckFall()
    {
        music.Stop();
        border.SetActive(false);
        Debug.Log("still brokey");
        disableFarmers();
    }

    private void levelComplete()
    {
        fade.SetActive(true);
        duckInstance.GetComponent<CircleCollider2D>().enabled = false;
        StartCoroutine(startNext());

    }

    private IEnumerator startNext()
    {
        Debug.Log("waiting");
        score++;
        
        disableFarmers();
        yield return new WaitForSeconds(1f);
        killFarmers();
        reset?.Invoke();
        duckInstance.transform.position = duckSpawn;
        scoreText.text = score + "";
        difficulty = score * (0.5f + (Mathf.Pow(0.9f, score) * 0.5f));
        spawnFarmers(difficulty + 1);
        disableFarmers();
        music.pitch = 1 + 0.0025f * difficulty;
        yield return new WaitForSeconds(1f);
        enableFarmers();
        fade.SetActive(false);
        duckInstance.GetComponent<CircleCollider2D>().enabled = true;
    }

    private void playerDied()
    {
        
        menu.SetActive(false);
        gameUI.SetActive(false);
        endScreen.SetActive(true);
        scoreTextEnd.text = score + "";
        Destroy(duckInstance);
        killFarmers();
        pauseButton.Disable();
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
        score = 0;
        border.SetActive(true);
        Time.timeScale = 1;
        scoreText.text = score + "";
        duckEvents.hit -= duckFall;
        duckEvents.hit += duckFall;
        duckInstance = null;
        duckInstance = Instantiate(duck, duckSpawn, Quaternion.identity);
        menu.SetActive(false);
        gameUI.SetActive(true);
        endScreen.SetActive(false);

        spawnFarmers(score+1);
        paused = false;
        pauseButton.Enable();
        music.Play();
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

            var fsmg = farmerInstance.GetComponent<SMGFarmer>();
            if (fsmg != null)
            {
                fsmg.enabled = false;
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

            var fsmg = farmerInstance.GetComponent<SMGFarmer>();
            if (fsmg != null)
            {
                fsmg.enabled = true;
            }

        }
    }

    public void returnToMainMenu()
    {
        score = 0;
        menu.SetActive(true);
        gameUI.SetActive(false);
        endScreen.SetActive(false);
        optionsMenu.SetActive(false);
        Destroy(duckInstance);
        killFarmers();
        reset?.Invoke();
        music.Stop();
        pauseButton.Disable();
    }

    public void openOptionsMenu()
    {
        menu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void resetGame()
    {
        
        Destroy(duckInstance);
        killFarmers();
        reset?.Invoke();
        startGame();
    }


}
