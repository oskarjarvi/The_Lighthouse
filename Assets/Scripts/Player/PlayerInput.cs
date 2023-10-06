using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerInput : MonoBehaviour
{
    private PlayerControls playerControls;
    private PlayerMovement playerMovement;
    private PlayerInteractions playerInteractions;
    private Lantern lanternHandler;
    private CameraController cameraController;
    private ItemInspector itemInspector;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerMovement = GetComponent<PlayerMovement>();
        playerInteractions = GetComponent<PlayerInteractions>();
        lanternHandler = GetComponent<Lantern>();
        cameraController = GetComponent<CameraController>();
        itemInspector = GetComponent<ItemInspector>();

        playerControls.PlayerInspect.Inspecting.started += StartInspecting;

        playerControls.PlayerInspect.Inspecting.performed += HandleInspecting;
        playerControls.PlayerInspect.Inspecting.canceled += StopInspecting;

        playerControls.PlayerInspect.Rotating.performed += HandleInspecting;

    }

    private void OnEnable()
    {
        playerControls.PlayerMovement.Enable();
        playerControls.PlayerMovement.Drop.Enable();
        playerControls.PlayerMovement.Walking.Enable();
        playerControls.PlayerMovement.Lighting.Enable();
        playerControls.PlayerMovement.Look.Enable();

    }

    private void OnDisable()
    {
        playerControls.PlayerMovement.Disable();
        playerControls.PlayerInspect.Inspecting.Disable();
        playerControls.PlayerMovement.Drop.Disable();
        playerControls.PlayerMovement.Walking.Disable();
        playerControls.PlayerMovement.Lighting.Disable();
        playerControls.PlayerMovement.Look.Disable();


    }

    private void Update()
    {
        HandleLantern();
        HandleInteract();
        HandleMovement();
        HandleDropItem();
        HandleCamera();
        if (playerInteractions.IsInspectingItem)
        {
            playerControls.PlayerMovement.Disable();
            playerControls.PlayerInspect.Enable();
        }
    }
    private void HandleLantern()
    {
        if(playerControls.PlayerMovement.Lighting.triggered)
        {
            lanternHandler.HandleLantern();
        }
    }

    private void HandleMovement()
    {
        Vector2 playerInput = playerControls.PlayerMovement.Walking.ReadValue<Vector2>();
        playerMovement.Move(playerInput);
    }
    private void HandleCamera()
    {
         Vector2 mouseCamPos= playerControls.PlayerMovement.Look.ReadValue<Vector2>();
        cameraController.HandleCameraMovement(mouseCamPos);
    }

    private void HandleInteract()
    {
        if (playerControls.PlayerMovement.Interacting.triggered)
        {
            playerInteractions.Interact();
        }
    }

    private void HandleInspecting(InputAction.CallbackContext context)
    {
        Vector2 mouseDelta = context.ReadValue<Vector2>();
        itemInspector.DragInspection(mouseDelta);
    }
    private void StartInspecting(InputAction.CallbackContext context)
    {
        itemInspector.StartRotation();
        Debug.Log("I started inspecting ");

    }
    private void StopInspecting(InputAction.CallbackContext context)
    {
        itemInspector.StopRotation();
        Debug.Log("I cancelled inspecting ");

    }

    private void HandleDropItem()
    {
        if (playerControls.PlayerMovement.Drop.triggered)
        {
            playerInteractions.DropItem();
        }
    }
}