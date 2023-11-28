using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using static UnityEngine.GraphicsBuffer;


public class PlayerInteractions : MonoBehaviour
{
    Player player;
    public Transform pickUpParent;

    public float maxInteractDistance;
    private InteractableItemBase _hitItem;

    private bool _isHittingItem = false;

    [SerializeField] private InteractionUIController _interactionUIController;


    private ItemInspector _itemInspector;


    private void Awake()
    {
        player = GetComponent<Player>();
        _itemInspector = GetComponent<ItemInspector>();

    }
    private void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!_itemInspector.IsInspecting)
        {
            //might have to do something here to avoid raycasting into picked up object.
            // int playerLayer = LayerMask.NameToLayer("Player");
            // int attachedObjectsLayer = LayerMask.NameToLayer("AttachedObjects");

            //int layerMask = ~(1 << playerLayer | 1 << attachedObjectsLayer);

            if (Physics.Raycast(ray, out hit, maxInteractDistance))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Interactable"))
                {

                    _hitItem = hit.collider.GetComponent<InteractableItemBase>();

                    _isHittingItem = true;


                    if (!_hitItem.Interacted && _isHittingItem)
                    {
                        _interactionUIController.SetUp(_hitItem.InteractionPrompt);
                    }
                }
                else
                {
                    _isHittingItem = false;
                    _interactionUIController.Close();
                    _hitItem = null;
                }

            }


        }
    }




    public void DropItem()
    {
        if (player.heldItem != null)
        {
            player.heldItem.DropItem();
        }
    }

    public void Interact()
    {
        if (_hitItem != null && _isHittingItem && !_hitItem.Interacted)
        {
            _hitItem.Interact();
        }
    }

}
