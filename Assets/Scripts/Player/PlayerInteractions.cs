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
    CameraController cameractrl;

    private void Awake()
    {
        player = GetComponent<Player>();
        cameractrl = GetComponent<CameraController>();
        itemInspector = GetComponent<ItemInspector>();
    }

    public void Interact()
    {
        Debug.Log("im in here");
        if(cameractrl != null)
        {
             if (cameractrl.IsHittingItem && player.heldItem == null)
             {
                    if(cameractrl.HitItem.isInspectable)
                    {
                        InspectItem(cameractrl.HitItem);
                    }
                    else
                    {
                        PickUpItem(cameractrl.HitItem);

                    }
             }
        }
       
    }
    public void PickUpItem(Item item)
    {
        if(player.heldItem == null)
        {
            player.heldItem = item;
            item.isPickedUp = true;

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
        itemInspector.InspectItem(item);
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
