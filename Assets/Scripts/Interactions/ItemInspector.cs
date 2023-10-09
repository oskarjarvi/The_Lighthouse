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


    
    public void StartInspectItem(InspectableItem item)
    {
        _isInspecting = true;
        var itemRb = item.GetComponent<Rigidbody>();

        itemRb.useGravity = false;

        inspectionCanvas.gameObject.SetActive(true);
        inspectCamera.gameObject.SetActive(true);

        //float cameraDistance = 2.0f; // Constant factor
        selectedPrefab = Instantiate(item.transform, inspectCamera.transform.position + inspectCamera.transform.forward * 2f, transform.rotation);

        //var bounds = selectedPrefab.GetComponent<MeshCollider>();

        //Vector3 objectSizes = bounds.max - bounds.min;
        //float objectSize = Mathf.Max(objectSizes.x, objectSizes.y, objectSizes.z);
        //float cameraView = 2.0f * Mathf.Tan(0.5f * Mathf.Deg2Rad * inspectCamera.fieldOfView); // Visible height 1 meter in front
        //float distance = cameraDistance * objectSize / cameraView; // Combined wanted distance from the object
        //distance += 0.5f * objectSize; // Estimated offset from the center to the outside of the object
        //inspectCamera.transform.position = bounds.center - distance * inspectCamera.transform.forward;



        selectedPrefab.gameObject.layer = LayerMask.NameToLayer("ExaminedItem");
        foreach (Transform child in selectedPrefab.gameObject.transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("ExaminedItem");
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
            Debug.Log(mouseInput);

            Vector3 rotationAmount = new Vector3(mouseInput.y, -mouseInput.x,0) * rotationSpeed * Time.deltaTime;
            selectedPrefab.transform.Rotate(rotationAmount, Space.World);


        }
    }
    public void StopRotation()
    {
        _isRotating = false;

    }

    private float CalculateScaleFactor(float objectSize, float cameraFOV)
    {
        // Calculate the scaling factor based on the object's size and camera field of view
        // Adjust the formula as needed to achieve the desired scaling behavior
        float desiredScaleFactor = Mathf.Tan(cameraFOV * 0.5f * Mathf.Deg2Rad) * objectSize * 2.0f;
        return desiredScaleFactor;
    }


    public void CancelInspection()
    {
        _isInspecting = false;
        inspectionCanvas.gameObject.SetActive(false);

        inspectCamera.gameObject.SetActive(false);

        Destroy(selectedPrefab.gameObject);
    }
}
