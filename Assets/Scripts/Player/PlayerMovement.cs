using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Player player;
    public float movementSpeed;
    Vector3 movementDirection;
    private float gravity = -9.8f;
    private float groundedGravity = 0.2f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float _velocity;


    private void Awake()
    {
        player = GetComponent<Player>();

    }
    private void Update()
    {
        handleGravity();
    }

    public void handleGravity()
    {
        if (player != null)
        {
            _velocity += (player.ctrl.isGrounded ? groundedGravity : gravity) * gravityMultiplier * Time.deltaTime;
        }
       
    }
    public void move(Vector2 moveInput)
    {
        movementDirection = new(moveInput.x, _velocity, moveInput.y);
        float cameraRot = Camera.main.transform.rotation.eulerAngles.y;

        Vector3 targetDirection = Quaternion.Euler(0, cameraRot, 0) * movementDirection * movementSpeed * Time.deltaTime;

        player.ctrl.Move(targetDirection);
    }
}
