using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;


public class PlayerInteractions : MonoBehaviour
{
    Player player;
    public Transform pickUpParent;
    public LayerMask interactableLayerMask;

    public float maxInteractDistance;
    private InteractableItemBase _hitItem;

    private bool _isHittingItem = false;

    [SerializeField] private InteractionUIController _interactionUIController;


    private void Awake()
    {
        player = GetComponent<Player>();

    }
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxInteractDistance, interactableLayerMask))
        {
            _hitItem = hit.collider.GetComponent<InteractableItemBase>();

            _isHittingItem = true;
            if(!_hitItem.Interacted)
            {
                _interactionUIController.SetUp(_hitItem.InteractionPrompt);

            }

        }
        else
        {
            _interactionUIController.Close();
            _isHittingItem = false;
            _hitItem = null;
        }
        
    }
   
    public void DropItem()
    {
        if(player.heldItem != null)
        {
            player.heldItem.DropItem();
        }
    }

    public void Interact()
    {
        if ( _hitItem != null && _isHittingItem && !_hitItem.Interacted)
        {
            _hitItem.Interact();
        }
    }

}
