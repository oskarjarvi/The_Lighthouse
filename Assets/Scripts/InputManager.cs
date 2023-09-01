//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class InputManager : MonoBehaviour
//{
//    [SerializeField] Player_Controller movement;
    
//    PlayerControls controls;
//    PlayerControls.PlayerActions player;

//    Vector2 horizontalInput;
//    private void Awake()
//    {
//        controls = new PlayerControls();
//        player = controls.Player;

//        player.Move.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();
//    }
//    private void OnEnable()
//    {
//        controls.Enable();
//    }
//    private void OnDisable()
//    {
//        controls.Disable(); 
//    }
//    // Start is called before the first frame update
//    //void Start()
//    //{

//    //}

//    // Update is called once per frame
//    void Update()
//    {
//        movement.ReceiveInput(horizontalInput);
//    }
//}
