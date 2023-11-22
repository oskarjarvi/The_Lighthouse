using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InspectableItem : MonoBehaviour, InteractableItemBase
{
    private ItemInspector inspector;
    [SerializeField]
    private Rigidbody _rb;
    [SerializeField]
    private string _prompt;
    public Rigidbody rb => _rb;

    public string InteractionPrompt => _prompt;

    public bool Interacted => false;

    [SerializeField] List<Material> _originalMaterials;

    [SerializeField] List<Material> _highlightMaterials;

    private MeshRenderer _renderer;

    private MeshRenderer[] _childRenderers;

    public GameObject page;

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _childRenderers = GetComponentsInChildren<MeshRenderer>();
        GameObject playerObject = GameObject.Find("Player");

        if (playerObject != null)
        {
            inspector = playerObject.GetComponent<ItemInspector>();
        }
    }
    public void Interact()
    {
        if(inspector != null)
        {
            inspector.StartInspectItem(this);

        }

    }


}
