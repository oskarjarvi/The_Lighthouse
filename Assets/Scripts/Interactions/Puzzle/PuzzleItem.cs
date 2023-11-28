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

    public Vector3 originalScale;

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
        if (CompareTag("LighthouseLens"))
        {
            float LensScale = 15f;
            transform.localScale = new Vector3(LensScale, LensScale, LensScale);
        }

        player.heldItem = this;

        transform.SetParent(heldItemSlot, false);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;


        rb.isKinematic = true;

        Collider itemCollider = GetComponent<Collider>();

        if (itemCollider != null)
        {
            itemCollider.enabled = false;
        }

    }
    public void DropItem()
    {
        Collider itemCollider = GetComponent<Collider>();



        if (itemCollider != null)
        {
            itemCollider.enabled = true;
            if(itemCollider.isTrigger)
            {
                itemCollider.isTrigger = false;

            }

        }
        transform.SetParent(null);
        transform.localScale = originalScale;

        rb.isKinematic = false;
        rb.useGravity = true;


        player.heldItem = null;
    }



}

