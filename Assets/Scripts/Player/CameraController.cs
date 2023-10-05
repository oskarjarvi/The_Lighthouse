using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private PlayerControls input;



    public float mouseSensitivity = 100f;
    public LayerMask interactableLayerMask;


    [SerializeField] float minViewAngle = 45f;
    [SerializeField] Transform playerBody;

    private Vector2 mouselookPos;

    float xRotation = 0f;
    private bool _isHittingItem = false;
    private Item _hitItem = null;
    public bool IsHittingItem { get { return _isHittingItem; } }

    public Item HitItem { get { return _hitItem; } } 


    private void Awake()
    {
        input = new PlayerControls();
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnEnable()
    {
        input.PlayerMovement.Look.Enable();
    }
    private void OnDisable()
    {
        input.PlayerMovement.Look.Disable();
    }
    private void look()
    {
        mouselookPos = input.PlayerMovement.Look.ReadValue<Vector2>();

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

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //check if it hit something with the interactable Layer within max distance (100)
        if (Physics.Raycast(ray, out hit, 100, interactableLayerMask))
        {
            if (hit.collider != null)
            {

                _isHittingItem = true;
                _hitItem = hit.collider.GetComponent<Item>();
            }
                
        }
    }

}
