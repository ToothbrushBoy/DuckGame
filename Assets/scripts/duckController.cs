using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class duckController : MonoBehaviour
{
    public Controls _controls;
    public float speedCap;
    public float acc;
    public Rigidbody2D rb;
    Vector2 moveDir = Vector2.zero; // normalised vector containing wasd direction
    private InputAction move;

    private void Awake()
    {
        _controls = new Controls();
    }

    public void OnEnable()
    {
        move = _controls.duck.movement;
        move.Enable();
    }

    public void OnDisable()
    {
        move.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveDir = move.ReadValue<Vector2>();
        rb.AddForce(moveDir * acc);
        Debug.Log(rb.velocity.magnitude);
        if(rb.velocity.magnitude > speedCap)
        {
            rb.velocity = moveDir * speedCap;
        }
    }
}
