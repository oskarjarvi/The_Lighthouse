using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemInspector : MonoBehaviour
{
    private Transform selectedPrefab;
    public Camera inspectCamera;
    public Canvas inspectionCanvas;

    private float rotationSpeed = 100.0f;
    private bool _isRotating = false;


    private bool _isInspecting = false;
    public bool IsInspecting { get { return _isInspecting; } set { _isInspecting = value; } }

    [SerializeField]
    InteractionUIController _interactionUIController;



    public void StartInspectItem(InspectableItem item)
    {
        _isInspecting = true;


        item.rb.useGravity = false;

        inspectionCanvas.gameObject.SetActive(true);
        inspectCamera.gameObject.SetActive(true);

        selectedPrefab = Instantiate(item.transform, inspectCamera.transform.position + inspectCamera.transform.forward * 2f, transform.rotation);

        selectedPrefab.gameObject.layer = LayerMask.NameToLayer("ExaminedItem");

        foreach (Transform child in selectedPrefab.gameObject.transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("ExaminedItem");
        }
        _interactionUIController.gameObject.SetActive(false);


    }


    public void StartRotation()
    {
        _isRotating = true;


    }
    public void DragInspection(Vector2 mouseInput)
    {
        if (_isRotating)
        {
            Debug.Log(mouseInput);

            Vector3 rotationAmount = new Vector3(mouseInput.y, -mouseInput.x, 0) * rotationSpeed * Time.deltaTime;
            selectedPrefab.transform.Rotate(rotationAmount, Space.World);


        }
    }
    public void StopRotation()
    {
        _isRotating = false;

    }
    public void CancelInspection()
    {
        _isInspecting = false;
        inspectionCanvas.gameObject.SetActive(false);

        inspectCamera.gameObject.SetActive(false);
        Destroy(selectedPrefab.gameObject);
        _interactionUIController.gameObject.SetActive(true);

    }
}
