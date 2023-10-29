using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSlot : InteractableItemBase
{
    public string expectedItemTag;
    public Player player;
    public Transform expectedItemSlot;
    private Animator animator;
    public String animatorBool;

    private void Awake()
    {
         animator = GetComponent<Animator>();
    }
    public override void Interact()
    {
        if(player.heldItem != null)
        {
           
          
            if (player.heldItem.CompareTag(expectedItemTag))
            {

                InteractableItemBase item = player.heldItem.GetComponent<InteractableItemBase>();


                item.transform.SetParent(expectedItemSlot, false);


                player.heldItem = null;

                //Trigger animation
                
                animator.SetBool(animatorBool, true);

            }
        }
        else
        {
            //trigger false animation/sound
            Debug.Log("Missing item");
        }
       
    }
}
