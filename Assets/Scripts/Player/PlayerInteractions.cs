using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;


public class PlayerInteractions : MonoBehaviour
{
    public LayerMask interactableLayerMask;
    Player player;
    public Transform pickUpParent;
    public Camera inspectCamera;

    private Collider playerCollider;

    private Transform selectedPrefab;




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

                if (item.isInspectable)
                {
                    Inspect(item);
                }
                else
                {
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


            }
            else if (player.heldItem != null)
            {
                Debug.Log("You already have an item");
                return;
            }
            return;

        }
        return;
    }
    public void Inspect(Item item)
    {
        item.rb.useGravity = false;
        //inspectCamera.


        if (selectedPrefab != null)
        {
            Destroy(selectedPrefab.gameObject);
        }

        


        selectedPrefab = Instantiate(item.prefab,inspectCamera.transform.position + inspectCamera.transform.forward *2f, Quaternion.identity);
        selectedPrefab.gameObject.layer = LayerMask.NameToLayer("ExaminedItem");

        foreach (Transform child in selectedPrefab.gameObject.transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("ExaminedItem");
        }

        //selectedPrefab.localPosition = Vector3.zero;
        //selectedPrefab.localRotation = Quaternion.identity;
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
