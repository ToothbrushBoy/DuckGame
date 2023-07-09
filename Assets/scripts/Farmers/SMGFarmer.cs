using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMGFarmer : MonoBehaviour
{
    public GameObject bullet;
    public GameObject duck;
    private float fireRate;
    public float defaultFireRate;
    public GameObject bulletSpawn;
    private float timer;
    private float reloadTimer;
    public float shootingTimer;
    private float bulletSpeed;
    public float defaultBulletSpeed;
    public float bulletScale;
    public float spread;
    public float pellets;
    List<GameObject> bullets;
    private float scaling;

    public Vector2 target = new Vector2(0, 0);
    public float duration;
    public float reloadTime;
    private bool isFirering = false;

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
        if (isFirering)
        {
            timer += Time.deltaTime;
            shootingTimer += Time.deltaTime;
            if (timer > fireRate)
            {
                timer = 0;
                fire();
            }
            if(shootingTimer > duration)
            {
                isFirering = false;
                timer = 0;
                shootingTimer = 0;
            }

        }
        else
        {
            reloadTimer += Time.deltaTime;
            if (reloadTimer > reloadTime)
            {
                timer = 0;
                reloadTimer = 0;
                isFirering = true;
            }
        }

        
    }


    void fire()
    {
        GetComponentInChildren<FarmerVisualsInterface>().muzzleFlash.Play();
        spawnBullet(Random.Range(-spread, spread));
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
