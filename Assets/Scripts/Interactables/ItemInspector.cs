using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemInspector : MonoBehaviour
{
    private Transform selectedPrefab;
    public Camera inspectCamera;

    private Vector2 lastMousePosition;
    private float rotationSpeed = 100.0f;
    private bool _isRotating = false;

    private bool _isInspecting = false;
    public bool IsInspecting { get { return _isInspecting; } set { _isInspecting = value; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartInspectItem(Item item)
    {

        item.rb.useGravity = false;

        if (selectedPrefab != null)
        {
            Destroy(selectedPrefab.gameObject);
        }
        inspectCamera.gameObject.SetActive(true);

        selectedPrefab = Instantiate(item.prefab, inspectCamera.transform.position + inspectCamera.transform.forward * 2f, Quaternion.identity);
        selectedPrefab.gameObject.layer = LayerMask.NameToLayer("ExaminedItem");

        foreach (Transform child in selectedPrefab.gameObject.transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("ExaminedItem");
        }
    }
    

    public void StartRotation()
    {
        _isRotating = true;

        lastMousePosition = Mouse.current.position.ReadValue();

    }
    public void DragInspection(Vector2 mouseInput)
    {
        if(_isRotating)
        {

            Vector3 rotationAmount = new Vector3(-mouseInput.y, mouseInput.x, 0) * rotationSpeed * Time.deltaTime;
            selectedPrefab.transform.Rotate(rotationAmount);

            lastMousePosition = Mouse.current.position.ReadValue();
        }
    }
    public void StopRotation()
    {
        _isRotating = false;
    }
}
