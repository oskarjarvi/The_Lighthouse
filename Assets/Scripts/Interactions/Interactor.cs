using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class Interactor : MonoBehaviour
{
    private RaycastHit target;
    private Ray interactRay;

    public LayerMask interactableLayerMask;


    private void Update()
    {
        interactRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(interactRay, out target, 100, interactableLayerMask))
        {
            if (target.collider != null)
            {
                var interactable = target.collider.GetComponent<IInteractable>();

                //_isHittingItem = true;
                //_hitItem = hit.collider.GetComponent<Item>();
            }

        }
    }
}
