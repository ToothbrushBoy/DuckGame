using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulllet : MonoBehaviour
{
    public float defaultSpeed;
    private float currentSpeed;
    private Rigidbody2D rb;
    public Vector2 target;
    public Vector2 dir;
    public Quaternion rot;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        currentSpeed = defaultSpeed;
        Vector2 pos = this.transform.position;
        duckEvents.hit += freeze;
        duckEvents.fall += unFreeze;
        ControllerMain.reset += selfDestruct;
        target = rot * target;
        transform.LookAt(target);
        dir = target - pos;
        dir = dir.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(dir * currentSpeed);
        if (rb.velocity.magnitude > currentSpeed)
        {
            rb.velocity = dir * currentSpeed;
            
        }

    }

    private void OnDisable()
    {
        duckEvents.hit -= freeze;
        duckEvents.fall -= unFreeze;
        ControllerMain.reset -= selfDestruct;
    }

    public void freeze()
    {
        currentSpeed = 0;
    }

    public void unFreeze()
    {
        currentSpeed = defaultSpeed;
    }

    public void selfDestruct()
    {
        Debug.Log("bullet kill");
        Destroy(this.gameObject);
    }
}
