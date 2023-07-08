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
    List<GameObject> bullets;
    // Start is called before the first frame update
    void Start()
    {
        duck = GameObject.FindGameObjectsWithTag("Player")[0];

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
        bullets = new List<GameObject>();
        bullets.Add(Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity));
        bullets.Add(Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity));
        bullets.Add(Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity));

        var rotation = Quaternion.AngleAxis(15, Vector3.forward);

        bullets[0].GetComponent<Bulllet>().target = duck.transform.position;
        bullets[0].GetComponent<Bulllet>().rot = Quaternion.identity;
        bullets[1].GetComponent<Bulllet>().target = duck.transform.position;
        bullets[1].GetComponent<Bulllet>().rot = Quaternion.AngleAxis(15, Vector3.forward);
        bullets[2].GetComponent<Bulllet>().target = duck.transform.position;
        bullets[2].GetComponent<Bulllet>().rot = Quaternion.AngleAxis(-15, Vector3.forward);

    }
}
