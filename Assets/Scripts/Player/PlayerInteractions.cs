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
    ItemInspector itemInspector;
    public Transform pickUpParent;
    public LayerMask interactableLayerMask;

    private bool _isHittingItem = false;
    private Item _hitItem = null;

    private bool _isInspectingItem = false;
    public bool IsInspectingItem {  get { return _isInspectingItem; } set { _isInspectingItem = value; } }

    private void Awake()
    {
        player = GetComponent<Player>();
        itemInspector = GetComponent<ItemInspector>();

    }

    public void Interact()
    {
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

        if (_isHittingItem && _hitItem != null && player.heldItem == null)
        {
            if (_hitItem.isInspectable)
            {
                InspectItem(_hitItem);
            }
            else
            {
                PickUpItem(_hitItem);

            }
        }


    }
    public void PickUpItem(Item item)
    {
        if (player.heldItem == null)
        {
            player.heldItem = item;
            item.IsPickedUp = true;

            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;

            item.transform.SetParent(pickUpParent, false);

            item.rb.isKinematic = true;

            Debug.Log(item.rb.isKinematic);
            if (player.GetComponent<Collider>() != null)
            {
                Physics.IgnoreCollision(player.GetComponent<Collider>(), item.itemCollider, true);
            }
        }
        else
        {
            DropItem();
            PickUpItem(item);
        }

    }
    public void InspectItem(Item item)
    {
        _isInspectingItem = true;
        itemInspector.StartInspectItem(item);
    }
    public void DropItem()
    {

        player.heldItem.transform.SetParent(null);

        player.heldItem.rb.isKinematic = false;
        player.heldItem.rb.useGravity = true;


        if (player != null)
        {
            Physics.IgnoreCollision(player.GetComponent<Collider>(), player.heldItem.itemCollider, false);

        }

        player.heldItem = null;

    }

}
