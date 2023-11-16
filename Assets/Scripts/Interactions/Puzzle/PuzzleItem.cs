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

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
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

        Vector3 currentScale = transform.localScale;
        Debug.Log(currentScale);


        transform.SetParent(heldItemSlot, false);

        transform.localScale = currentScale;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;


        rb.isKinematic = true;

        if (player.GetComponent<Collider>() != null)
        {
            Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>(), true);
        }

    }
    public void DropItem()
    {
        Vector3 currentScale = transform.localScale;

        // Set the parent to null (dropping the item)
        transform.SetParent(null);

        // Reset the scale after setting the parent to null
        transform.localScale = currentScale;

        rb.isKinematic = false;
        rb.useGravity = true;


        if (player != null)
        {
            Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>(), false);

        }

        player.heldItem = null;
    }



}

