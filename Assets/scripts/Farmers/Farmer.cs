using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet;
    public GameObject duck;
    private float fireRate;
    public float defualtFireRate;
    public GameObject bulletSpawn;
    public float timer;
    public float defaultBulletSpeed;
    private float bulletSpeed;
    public float bulletScale;
    private float scaling;
    void Start()
    {
        
        bulletSpeed = defaultBulletSpeed * scaling;
        fireRate = defualtFireRate / scaling;
        var ducks = GameObject.FindGameObjectsWithTag("Player");
        duck = ducks[ducks.Length - 1];
        GetComponentInChildren<FarmerVisualsInterface>().duck = duck.transform;
    }

    private void OnEnable()
    {
        
        scaling = GetComponent<Difficulty>().scaling;
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > fireRate)
        {
            timer = 0;
            fire();
        }
    }

    void fire()
    {
        spawnBullet();
    }

    void spawnBullet()
    {
        var towardDuck = (duck.transform.position - bulletSpawn.transform.position) / bulletSpeed;
        var whereDuckGoin = duck.transform.position + (Vector3)duck.GetComponent<Rigidbody2D>().velocity;
        var target = towardDuck + whereDuckGoin;


        GameObject firedBullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);
        firedBullet.GetComponent<Bulllet>().target = target;
        firedBullet.GetComponent<Bulllet>().rot = Quaternion.identity;
        firedBullet.GetComponent<Bulllet>().defaultSpeed = bulletSpeed;
        firedBullet.transform.localScale = new Vector3(1,1,1) * bulletScale;
    } 
}
