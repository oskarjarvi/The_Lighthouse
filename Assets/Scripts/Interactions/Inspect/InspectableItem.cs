using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
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
