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

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerMovement = GetComponent<PlayerMovement>();
        playerInteractions = GetComponent<PlayerInteractions>();
        lanternHandler = GetComponent<Lantern>();

        playerControls.PlayerInspect.Inspecting.started += StartInspecting;
        playerControls.PlayerInspect.Inspecting.performed += HandleInspecting;
        playerControls.PlayerInspect.Inspecting.canceled += StopInspecting;



    }

    private void OnEnable()
    {
        playerControls.PlayerMovement.Enable();
        playerControls.PlayerMovement.Drop.Enable();
        playerControls.PlayerMovement.Walking.Enable();
        playerControls.PlayerMovement.Lighting.Enable();

    }

    private void OnDisable()
    {
        playerControls.PlayerMovement.Disable();
        playerControls.PlayerInspect.Inspecting.Disable();
        playerControls.PlayerMovement.Drop.Disable();
        playerControls.PlayerMovement.Walking.Disable();
        playerControls.PlayerMovement.Lighting.Disable();

    }

    private void Update()
    {
        HandleLantern();
        HandleInteract();
        HandleMovement();
        HandleDropItem();
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

    private void HandleInteract()
    {
        if (playerControls.PlayerMovement.Interacting.triggered)
        {
            playerInteractions.Interact();
        }
    }
    private void HandleInspecting(InputAction.CallbackContext context)
    {
        Vector2 delta = Mouse.current.delta.ReadValue();

        //playerInteractions.InspectItems(delta);
    }
    private void StartInspecting(InputAction.CallbackContext context)
    {
        Debug.Log("Whats up, i started");

    }
    private void StopInspecting(InputAction.CallbackContext context)
    {
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