using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleItem : MonoBehaviour, InteractableItemBase
{
    [SerializeField] private Transform heldItemSlot;
    [SerializeField] Player player;
    [SerializeField] string _prompt;

    private Rigidbody _rb;
    public Rigidbody rb => _rb;

    public string InteractionPrompt => _prompt;
    public bool hasInteracted = false;
    public bool Interacted => hasInteracted;

    private Vector3 originalScale;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        originalScale = transform.localScale;

    }
    public void Interact()
    {

        if (player.heldItem == null)
        {
            PickUpItem();
        }

    }
    private void PickUpItem()
    {
        player.heldItem = this;




        transform.SetParent(heldItemSlot, false);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;


        rb.isKinematic = true;
        // possibly set the layer to a "pickedUpObject" layer here
        if (player.GetComponent<Collider>() != null)
        {
            Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>(), true);
        }

    }
    public void DropItem()
    {

        transform.SetParent(null);

        transform.localScale = originalScale;

        rb.isKinematic = false;
        rb.useGravity = true;


        if (player != null)
        {
            Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>(), false);

        }

        player.heldItem = null;
    }



}

