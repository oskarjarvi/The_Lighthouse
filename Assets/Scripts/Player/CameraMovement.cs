using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    private PlayerControls input;



    public float mouseSensitivity = 100f;

    [SerializeField] float minViewAngle = 45f;
    [SerializeField] Transform playerBody;

    private Vector2 mouselookPos;

    float xRotation = 0f;

    private void Awake()
    {
        input = new PlayerControls();
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnEnable()
    {
        input.PlayerInput.Look.Enable();
    }
    private void OnDisable()
    {
        input.PlayerInput.Look.Disable();
    }
    private void look()
    {
        mouselookPos = input.PlayerInput.Look.ReadValue<Vector2>();

        float mouseX = mouselookPos.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouselookPos.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -minViewAngle, minViewAngle);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        playerBody.Rotate(Vector3.up * mouseX);
    }
    private void Update()
    {
        look();
    }
}
