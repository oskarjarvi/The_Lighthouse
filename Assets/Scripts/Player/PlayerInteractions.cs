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
    public LayerMask interactableLayerMask;
    [SerializeField]
    Color oldEmissionColor;
    [SerializeField]
    Color newEmissionColor;
    [SerializeField]
    float emissionIntensity;

    public float maxInteractDistance;
    private InteractableItemBase _hitItem;

    private bool _isHittingItem = false;

    [SerializeField] private InteractionUIController _interactionUIController;

    List<Material> activeMaterials = new List<Material>();



    private void Awake()
    {
        player = GetComponent<Player>();


    }
    private void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxInteractDistance,interactableLayerMask))
        {
            _hitItem = hit.collider.GetComponent<InteractableItemBase>();

            _isHittingItem = true;
            MeshRenderer meshRenderer = hit.collider.GetComponent<MeshRenderer>();

           
            if (!_hitItem.Interacted && _isHittingItem)
            {
                _interactionUIController.SetUp(_hitItem.InteractionPrompt);
                if (meshRenderer != null)
                {
                    Material meshMat = meshRenderer.material;


                    ToggleEmission(meshMat);
                    activeMaterials.Add(meshMat);

                }
                else
                {
                    MeshRenderer[] childRenderers = hit.collider.GetComponentsInChildren<MeshRenderer>();
                    foreach (MeshRenderer childRenderer in childRenderers)
                    {
                        Material childMat = childRenderer.material; // Declare a local variable
                        ToggleEmission(childMat);
                        activeMaterials.Add(childMat);

                    }

                }

            }
        }

        else
        {
            foreach (Material activeMat in activeMaterials)
            {
                ResetEmission(activeMat);
            }

            _isHittingItem = false;
            _interactionUIController.Close();
            _hitItem = null;
        }

    }
    void ToggleEmission(Material emissionMaterial)
    {
        if(emissionMaterial != null)
        {
            emissionMaterial.SetColor("_EmissionColor", newEmissionColor * emissionIntensity);

            
            emissionMaterial.EnableKeyword("_EMISSION");

        }

    }
    void ResetEmission(Material emissionMaterial)
    {
        if(emissionMaterial != null)
        {
            emissionMaterial.SetColor("_EmissionColor", oldEmissionColor * 0f);

            // Disable emission globally for the material
            emissionMaterial.DisableKeyword("_EMISSION");
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
