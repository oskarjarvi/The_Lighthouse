using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Player player;
    public float movementSpeed;
    Vector3 movementDirection;

    private void Awake()
    {
        player = GetComponent<Player>();

    }

    public void move(Vector2 moveInput)
    {
        movementDirection = new(moveInput.x, 0, moveInput.y);
        float cameraRot = Camera.main.transform.rotation.eulerAngles.y;

        player.rb.position += Quaternion.Euler(0, cameraRot, 0) * movementDirection * movementSpeed * Time.deltaTime;
    }
}
