using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectableItem : InteractableItemBase
{
    private ItemInspector inspector;

    private void Start()
    {
        GameObject playerObject = GameObject.Find("Player");

        if (playerObject != null)
        {
            inspector = playerObject.GetComponent<ItemInspector>();
        }
    }
    public override void Interact()
    {
        if(inspector != null)
        {
            inspector.StartInspectItem(this);
        }

    }

}
