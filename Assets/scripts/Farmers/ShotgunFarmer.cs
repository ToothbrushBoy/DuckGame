using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunFarmer : MonoBehaviour
{
    public GameObject bullet;
    public GameObject duck;
    public float fireRate;
    public GameObject bulletSpawn;
    public float timer;
    public float bulletSpeed;
    public float bulletScale;
    public float spread;
    public int pellets;
    List<GameObject> bullets;
    private float scaling;
    // Start is called before the first frame update
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
        if (timer > fireRate)
        {
            timer = 0;
            fire();
        }
    }


    void fire()
    {
        float angle = -spread * (pellets / 2 - 0.5f);
        for(int i = 0; i < pellets; i++)
        {
            spawnBullet(angle);
            angle += spread;
        }

    }

    void spawnBullet(float angle)
    { 
        GameObject firedBullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);
        firedBullet.GetComponent<Bulllet>().rot = Quaternion.AngleAxis(angle, Vector3.forward);
        firedBullet.GetComponent<Bulllet>().target = duck.transform.position;
        firedBullet.GetComponent<Bulllet>().defaultSpeed = bulletSpeed;
        firedBullet.transform.localScale = new Vector3(1, 1, 1) * bulletScale;
    }
}
