using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInteractions : MonoBehaviour
{
    public LayerMask interactableLayerMask;
    Player player;
    public Transform pickUpParent;

    public event EventHandler<Item> OnItemSelected;

    private void Awake()
    {
        player = GetComponent<Player>();

    }
    // Update is called once per frame
  
    public void interact()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //check if it hit something with the interactable Layer within max distance (100)
        if (Physics.Raycast(ray, out hit, 100, interactableLayerMask))
        {
            //If you hit an object in the interactable layer, check if it has a collider
            if (hit.collider != null)
            {
                var item = hit.collider.GetComponent<Item>();

                player.heldItem = item;
                item.isPickedUp = true;

                item.transform.position = Vector3.zero;
                item.transform.rotation = Quaternion.identity;

                item.transform.SetParent(pickUpParent, false);

                item.rb.isKinematic = true;
            }

        }
        return;
    }
    public void inspect()
    {
        
    }
}
