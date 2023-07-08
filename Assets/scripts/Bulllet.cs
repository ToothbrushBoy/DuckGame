using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulllet : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public Vector2 target;
    public Vector2 dir;
    public Quaternion rot;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        Vector2 pos = this.transform.position;
        target = rot * target;
        this.transform.LookAt(target);
        dir = target - pos;
        dir = dir.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(dir * speed);
        if (rb.velocity.magnitude > speed)
        {
            rb.velocity = dir * speed;
            
        }

    }
}
