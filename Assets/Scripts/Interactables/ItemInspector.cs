using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInspector : MonoBehaviour
{
    private Transform selectedPrefab;
    public Camera inspectCamera;

    private bool _isInspecting = false;
    public bool IsInspecting { get { return _isInspecting; } set { _isInspecting = value; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InspectItem(Item item)
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
}
