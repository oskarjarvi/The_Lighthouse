using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PuzzleSlot : MonoBehaviour, InteractableItemBase
{
    [SerializeField]
    private string _prompt;
    [SerializeField]
    private string failPrompt;

    public string expectedItemTag;

    public Player player;
    public Transform expectedItemSlot;
    private Animator animator;
    public string animatorBool;

    public Rigidbody rb => null;

    public string InteractionPrompt => _prompt;

    public bool hasInteracted = false;

    public bool Interacted => hasInteracted;

    public Animator popupAnimator;






    private void Awake()
    {
        animator = GetComponent<Animator>();

    }
    public void Interact()
    {
        if (player.heldItem != null)
        {

            if (player.heldItem.CompareTag(expectedItemTag))
            {

                PuzzleItem item = player.heldItem.GetComponent<PuzzleItem>();

                item.hasInteracted = true;
                item.transform.SetParent(expectedItemSlot, false);


                player.heldItem = null;

                //Trigger animation
                if (animator != null)
                {
                    animator.SetBool(animatorBool, true);

                }
                hasInteracted = true;
            }
        }

        else
        {
            //trigger false animation/sound
            PopUp();
        }

    }
    public void PopUp()
    {
        //tmpText = failPrompt;

    }

}
