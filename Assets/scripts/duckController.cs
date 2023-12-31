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
    public bool dead;


    Vector2 moveDir = Vector2.zero; // normalised vector containing wasd direction
    private InputAction move;
    public bool hit;

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
        duckEvents.hit -= _hit;
        duckEvents.fall -= _fall;
    }

    // Start is called before the first frame update
    void Start()
    {
        duckEvents.hit += _hit;
        duckEvents.fall += _fall;
        rb = GetComponent<Rigidbody2D>();
        hit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hit)
        {
            moveDir = move.ReadValue<Vector2>();
            rb.velocity = moveDir.normalized * speedCap;
            if (rb.velocity.magnitude > speedCap)
            {
                rb.velocity = moveDir * speedCap;
            }

            if (moveDir.x < 0) // I'll probably redo it and it'll look much better
            {
                animator.SetBool("left", true);
            }
            else
            {
                animator.SetBool("left", false);
            }
            if (moveDir.y == 0)
            {
                animator.SetBool("up", false);
                animator.SetBool("down", false);
            }
            else if (moveDir.y > 0)
            {
                animator.SetBool("up", true);
                animator.SetBool("down", false);
            }
            else
            {
                animator.SetBool("up", false);
                animator.SetBool("down", true);
            }

        }
       

    }

    void _hit()
    {
        hit = true;
        rb.velocity = moveDir * 0;
        animator.SetTrigger("DuckHit");
    }
    void _fall()
    {
        animator.SetTrigger("DuckFall");
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -2);
    }
}
