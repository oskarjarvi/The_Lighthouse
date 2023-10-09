using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleItem : InteractableItemBase
{
    [SerializeField] private Transform heldItemSlot;
    [SerializeField] Player player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public override void Interact()
    {
        if(player.heldItem == null)
        {
            PickUpItem();
        }

    }

    private void PickUpItem()
    {
            player.heldItem = this;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            transform.SetParent(heldItemSlot, false);

            rb.isKinematic = true;

            if (player.GetComponent<Collider>() != null)
            {
                Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>(), true);
            }
        
    }
    public void DropItem()
    {
        transform.SetParent(null);

        rb.isKinematic = false;
        rb.useGravity = true;


        if (player != null)
        {
            Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>(), false);

        }

        player.heldItem = null;
    }
    private void PlacePuzzleItem()
    {

    }
}

