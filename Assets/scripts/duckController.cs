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
    public Animator animator;

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
        if(rb.velocity.magnitude > speedCap)
        {
            rb.velocity = moveDir * speedCap;
        }

        if(moveDir.x < 0)
        {
            animator.SetBool("left", true);
        } else
        {
            animator.SetBool("left", false);
        }
        if(moveDir.y == 0)
        {
            animator.SetBool("up", false);
            animator.SetBool("down", false);
        } else if (moveDir.y > 0)
        {
            animator.SetBool("up", true);
            animator.SetBool("down", false);
        } else
        {
            animator.SetBool("up", false);
            animator.SetBool("down", true);
        }

    }
}
