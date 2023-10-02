using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerControls playerControls;
    private PlayerMovement playerMovement;
    private PlayerInteractions playerInteractions;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerMovement = GetComponent<PlayerMovement>();
        playerInteractions = GetComponent<PlayerInteractions>();
    }

    private void OnEnable()
    {
        playerControls.PlayerInput.Enable();
        playerControls.PlayerInput.Interacting.Enable();
        playerControls.PlayerInput.Drop.Enable();
    }

    private void OnDisable()
    {
        playerControls.PlayerInput.Disable();
        playerControls.PlayerInput.Interacting.Disable();
        playerControls.PlayerInput.Drop.Disable();
    }

    private void Update()
    {
        HandleMovement();
        HandleInteract();
        HandleDropItem();
    }

    private void HandleMovement()
    {
        Vector2 playerInput = playerControls.PlayerInput.Walking.ReadValue<Vector2>();
        playerMovement.Move(playerInput);
    }

    private void HandleInteract()
    {
        if (playerControls.PlayerInput.Interacting.triggered)
        {
            playerInteractions.Interact();
        }
    }

    

    private void HandleDropItem()
    {
        if (playerControls.PlayerInput.Drop.triggered)
        {
            playerInteractions.DropItem();
        }
    }
}