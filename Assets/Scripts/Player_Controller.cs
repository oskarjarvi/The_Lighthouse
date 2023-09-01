using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 10f;

    public InputAction controls;

    Vector3 moveDirection = Vector3.zero;

    private void OnEnable()
    {
        controls.Enable();

    }
    private void OnDisable()
    {
        controls.Disable();
    }
    private void Update()
    {
        moveDirection = controls.ReadValue<Vector3>();
        print(moveDirection);
    }
    private void FixedUpdate()
    {

        rb.velocity = new Vector3(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed, moveDirection.z * moveSpeed);
    }
   
}
