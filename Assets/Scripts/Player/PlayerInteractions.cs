using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;


public class PlayerInteractions : MonoBehaviour
{
    Player player;
    public Transform pickUpParent;
    public LayerMask interactableLayerMask;
    private int highlightLayer;
    private int interactableLayer;


    public float maxInteractDistance;
    private InteractableItemBase _hitItem;

    private bool _isHittingItem = false;

    [SerializeField] private InteractionUIController _interactionUIController;

    private GameObject _currentTarget = null;


    private void Awake()
    {
        player = GetComponent<Player>();
        highlightLayer = LayerMask.NameToLayer("Highlight");
        interactableLayer = LayerMask.NameToLayer("Interactable");


    }
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxInteractDistance, LayerMask.GetMask("Highlight","Interactable")))
        {
            GameObject target = hit.collider.gameObject;

            _hitItem = hit.collider.GetComponent<InteractableItemBase>();

            _isHittingItem = true;
            if (!_hitItem.Interacted && _isHittingItem && _currentTarget != target)
            {
                _interactionUIController.SetUp(_hitItem.InteractionPrompt);
               
                    Debug.Log("Yo im in here");
                    _currentTarget = target;
                    _currentTarget.layer = highlightLayer;
            }
        }

        else
        {
            _isHittingItem = false;

            if (_currentTarget != null && !_isHittingItem)
            {
                _currentTarget.layer = interactableLayer;
                _currentTarget = null;
            }
            _interactionUIController.Close();
            _hitItem = null;
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
