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

    public GameObject inspectGUI;

    private float rotationSpeed = 100.0f;
    private bool _isRotating = false;


    private bool _isInspecting = false;
    public bool IsInspecting { get { return _isInspecting; } set { _isInspecting = value; } }

    [SerializeField]
    InteractionUIController _interactionUIController;

    [SerializeField]
    CursorManager cursorManager;

    public bool isTextVisible = false;
    private InspectableItem selectedItem;
    


    public void StartInspectItem(InspectableItem item)
    {
        _isInspecting = true;
        Quaternion rotation = Quaternion.Euler(90f, 180f, 0f);
        if (cursorManager != null)
        {
            cursorManager.SetCursorState(true);
        }

        item.rb.useGravity = false;

        inspectionCanvas.gameObject.SetActive(true);
        inspectCamera.gameObject.SetActive(true);
        inspectGUI.SetActive(true);


        selectedPrefab = Instantiate(item.transform, inspectCamera.transform.position + inspectCamera.transform.forward * 1f, rotation);

        selectedPrefab.gameObject.layer = LayerMask.NameToLayer("ExaminedItem");

        foreach (Transform child in selectedPrefab.gameObject.transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("ExaminedItem");
        }
        _interactionUIController.gameObject.SetActive(false);

        selectedItem = selectedPrefab.GetComponent<InspectableItem>();

    }
    public void ToggleText()
    {

        if (selectedItem != null && selectedItem.page != null)
        {
            selectedItem.page.SetActive(!selectedItem.page.activeSelf);
            isTextVisible = !isTextVisible;

        }
    }


    public void StartRotation()
    {
        _isRotating = true;


    }
    public void DragInspection(Vector2 mouseInput)
    {
        if (_isRotating)
        {

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

        inspectGUI.SetActive(false);
        if (cursorManager != null)
        {
            cursorManager.SetCursorState(false);
        }
        selectedItem.page.SetActive(false);
        isTextVisible = false;

        _isInspecting = false;
        inspectionCanvas.gameObject.SetActive(false);

        inspectCamera.gameObject.SetActive(false);
        Destroy(selectedPrefab.gameObject);
        _interactionUIController.gameObject.SetActive(true);

    }
}
