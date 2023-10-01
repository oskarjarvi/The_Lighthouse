using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInput : MonoBehaviour
{
    public PlayerControls input;
    
    private PlayerMovement playerMovement;
    private PlayerInteractions playerInteractions;


    private void Awake()
    {
       
        playerMovement = GetComponent<PlayerMovement>();
        playerInteractions = GetComponent<PlayerInteractions>();
        input = new PlayerControls();
    }

    private void OnEnable()
    {


        input.PlayerInput.Enable();
        input.PlayerInput.Interacting.Enable();
        input.PlayerInput.Drop.Enable();

    }
    private void OnDisable()
    {

        input.PlayerInput.Disable();
        input.PlayerInput.Interacting.Disable();
        input.PlayerInput.Drop.Disable();


    }
    private void Update()
    {
        handleMovement();
        interact();
        inspect();
        dropItem();
    }
   
    void handleMovement()
    {
        //Read the input coming from unity, its listening for the WASD as specified in the input map. This is only in X and Y axis since its a Vector2(e.g 2d)
        Vector2 playerInput = input.PlayerInput.Walking.ReadValue<Vector2>();

        //Let the playerMovement script handle the rest
        playerMovement.move(playerInput);

    }
    //interact with objects
    void interact()
    {
        if (input.PlayerInput.Interacting.triggered)
        {
            playerInteractions.interact();
        }
           
    }
    void inspect()
    {
        if(input.PlayerInput.Inspecting.triggered)
        {

            playerInteractions.inspect();

        }
    }
    void dropItem()
    {
        if (input.PlayerInput.Drop.triggered)
        {
            playerInteractions.DropItem();
        }
    }


}
