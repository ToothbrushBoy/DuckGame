using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class duckController : MonoBehaviour
{
    public Controls _controls;

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
    }
}
