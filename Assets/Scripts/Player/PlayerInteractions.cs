using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;


public class PlayerInteractions : MonoBehaviour
{
    Player player;
    public Transform pickUpParent;
    public LayerMask interactableLayerMask;

    private IInteractable _hitItem;

    private bool _isHittingItem = false;


    private void Awake()
    {
        player = GetComponent<Player>();

    }
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, interactableLayerMask))
        {
            if (hit.collider != null)
            {
                _hitItem = hit.collider.GetComponent<IInteractable>();

                _isHittingItem = true;
            }

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
        if ( _hitItem != null && player.heldItem == null)
        {
            _hitItem.Interact();
        }
    }

}
