using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet;
    public GameObject duck;
    public float fireRate;
    public GameObject bulletSpawn;
    public float timer;
    public float bulletSpeed;
    public float bulletScale;
    private float scaling;
    void Start()
    {
        duck = GameObject.FindGameObjectsWithTag("Player")[0];
        
        bulletSpeed = bulletSpeed * scaling;
        fireRate = fireRate / scaling;
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
        GameObject firedBullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);
        firedBullet.GetComponent<Bulllet>().target = duck.transform.position;
        firedBullet.GetComponent<Bulllet>().rot = Quaternion.identity;
        firedBullet.GetComponent<Bulllet>().defaultSpeed = bulletSpeed;
        firedBullet.transform.localScale = new Vector3(1,1,1) * bulletScale;
    } 
}
