using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInteractions : MonoBehaviour
{
    public LayerMask interactableLayerMask;
    Player player;
    public Transform pickUpParent;

    private Collider playerCollider;

    public event EventHandler<Item> OnItemSelected;

    private void Awake()
    {
        player = GetComponent<Player>();
        playerCollider = player.ctrl.gameObject.GetComponent<Collider>();

    }

    public void Interact()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //check if it hit something with the interactable Layer within max distance (100)
        if (Physics.Raycast(ray, out hit, 100, interactableLayerMask))
        {
            //If you hit an object in the interactable layer, check if it has a collider
            if (hit.collider != null && player.heldItem == null)
            {
                var item = hit.collider.GetComponent<Item>();

                player.heldItem = item;
                item.isPickedUp = true;

                item.transform.localPosition = Vector3.zero;
                item.transform.localRotation = Quaternion.identity;

                item.transform.SetParent(pickUpParent, false);

                item.rb.isKinematic = true;

                Debug.Log(item.rb.isKinematic);
                if (playerCollider != null)
                {
                    Physics.IgnoreCollision(playerCollider, item.GetComponent<Collider>(), true);

                }

            }
            else if(player.heldItem != null)
            {
                Debug.Log("You already have an item");
                return;
            }
            return;

        }
        return;
    }
    public void Inspect()
    {
        
    }
    public void DropItem()
    {
        if (player.heldItem != null)
        {
            player.heldItem.transform.SetParent(null);

            player.heldItem.rb.isKinematic = false;
            player.heldItem.rb.useGravity = true;


            if (playerCollider != null)
            {
                Physics.IgnoreCollision(playerCollider, player.heldItem.GetComponent<Collider>(), false);

            }

            player.heldItem = null;
        }
    }
}
