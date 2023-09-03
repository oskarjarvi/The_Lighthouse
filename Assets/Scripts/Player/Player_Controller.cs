using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    public PlayerControls input;
    public Rigidbody rb;

    public float moveSpeed = 10f;

    Vector3 movementDirection;

    private void Awake()
    {
        input = new PlayerControls();

    }

    private void OnEnable()
    {
        input.PlayerInput.Enable();

    }
    private void OnDisable()
    {
        input.PlayerInput.Disable();
    }
    private void Update()
    {
        handleMovement();
        
    }
   
    void handleMovement()
    {
        //Read the input coming from unity, its listening for the WASD as specified in the input map. This is only in X and Y axis since its a Vector2(e.g 2d)
        Vector2 playerInput = input.PlayerInput.Walking.ReadValue<Vector2>();

        //Set the movement Direction to match a 3d space (Vector3), e.g "up" is forward rather than up. 
        movementDirection = new(playerInput.x, 0, playerInput.y);
       
        //Get the camera rotation
        float cameraRot = Camera.main.transform.rotation.eulerAngles.y;
        //Apply the new position with the correct rotation, movement speed and Direction.
        rb.position += Quaternion.Euler(0, cameraRot, 0) * movementDirection * moveSpeed * Time.deltaTime;

    }

    private void FixedUpdate()
    {
        interact();
        inspect();
    }
    //interact with objects
    void interact()
    {
        //cast out a ray from the mouseposition originating from the player
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //check if it hit something within max distance (100)
        if (input.PlayerInput.Interacting.triggered &&  Physics.Raycast(ray,out hit, 100))
        {
            Debug.Log(hit.rigidbody);

            //pick up object

        }
    }
    void inspect()
    {
        if(input.PlayerInput.Inspecting.triggered)
        {

        }
    }
    

}
