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

    private InteractableItemBase _hitItem;

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
                _hitItem = hit.collider.GetComponent<InteractableItemBase>();

                _isHittingItem = true;
            }

        }
        else
        {
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
        if ( _hitItem != null && _isHittingItem)
        {
            _hitItem.Interact();
        }
    }

}
