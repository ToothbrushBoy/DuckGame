using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunFarmer : MonoBehaviour
{
    public GameObject bullet;
    public GameObject duck;
    private float fireRate;
    public float defaultFireRate;
    public GameObject bulletSpawn;
    public float timer;
    private float bulletSpeed;
    public float defaultBulletSpeed;
    public float bulletScale;
    public float spread;
    public float pellets;
    List<GameObject> bullets;
    private float scaling;
    // Start is called before the first frame update
    void Start()
    {
        var ducks = GameObject.FindGameObjectsWithTag("Player");
        duck = ducks[ducks.Length - 1];
        bulletSpeed = defaultBulletSpeed * scaling;
        fireRate = defaultFireRate / scaling;
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
        if (timer > fireRate)
        {
            timer = 0;
            fire();
        }
    }


    void fire()
    {
        GetComponentInChildren<FarmerVisualsInterface>().muzzleFlash.Play();
        Debug.Log("inital angle multiplier" + (pellets / 2 - 0.5f));
        float angle = -spread * (pellets / 2 - 0.5f);
        for(int i = 0; i < pellets; i++)
        {
            Debug.Log("ANGLES:  " + angle);
            spawnBullet(angle);
            angle += spread;
        }

    }

    void spawnBullet(float angle)
    {
        var towardDuck = (duck.transform.position - bulletSpawn.transform.position) / bulletSpeed;
        var whereDuckGoin = duck.transform.position + (Vector3)duck.GetComponent<Rigidbody2D>().velocity;
        var target = towardDuck + whereDuckGoin;

        GameObject firedBullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);
        firedBullet.GetComponent<Bulllet>().rot = Quaternion.AngleAxis(angle, Vector3.forward);
        firedBullet.GetComponent<Bulllet>().target = target;
        firedBullet.GetComponent<Bulllet>().defaultSpeed = bulletSpeed;
        firedBullet.transform.localScale = new Vector3(1, 1, 1) * bulletScale;
    }
}
