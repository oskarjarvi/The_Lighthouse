using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSlot : InteractableItemBase
{
    public string expectedItemTag;
    public Player player;
    public Transform expectedItemSlot;

    public override void Interact()
    {
        if(player.heldItem != null)
        {
            //Debug.Log("Expected Item InstanceID: " + expectedItem.GetInstanceID());
            //Debug.Log("Held Item InstanceID: " + player.heldItem.GetInstanceID());

            Debug.Log("im interacting");
            if (player.heldItem.CompareTag(expectedItemTag))
            {
                Debug.Log("I have item");

                InteractableItemBase item = player.heldItem.GetComponent<InteractableItemBase>();

                item.transform.SetParent(expectedItemSlot, false);

                player.heldItem = null;

                //Trigger animation
                transform.Rotate(new Vector3(0, -90, 0));

            }
        }
        else
        {
            //trigger false animation/sound
            Debug.Log("Missing item");
        }
       
    }
}
