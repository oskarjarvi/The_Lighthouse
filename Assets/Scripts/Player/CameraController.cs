using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    public float mouseSensitivity = 100f;
    public Camera playerCamera;

    [SerializeField] float minViewAngle = 45f;
    [SerializeField] Transform playerBody;
    [SerializeField] Transform lantern;

    private ItemInspector itemInspector;

    float xRotation = 0f;
    //private Vector3 lanternOffset;

    private void Awake()
    {
        itemInspector = GetComponent<ItemInspector>();
        Cursor.lockState = CursorLockMode.Locked;

        //lanternOffset = lantern.position - playerCamera.transform.position;

    }
    private void Update()
    {

        if (itemInspector.IsInspecting)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void HandleCameraMovement(Vector2 mouselookPos)
    {

        float mouseX = mouselookPos.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouselookPos.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -minViewAngle, minViewAngle);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        // Rotate the player's body only around the y-axis
        playerBody.Rotate(Vector3.up * mouseX, Space.World);


        lantern.rotation = Quaternion.Euler(0, playerCamera.transform.eulerAngles.y, 0);
    }

}
