using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPuzzle : MonoBehaviour, InteractableItemBase
{
    public Player player;

    public string puzzlePieceOneTag;
    public string puzzlePieceTwoTag;

    public Transform puzzlePieceOneSlot;
    public Transform puzzlePieceTwoSlot;

    private bool isPuzzleSlot1Filled = false;
    private bool isPuzzleSlot2Filled = false;


    public GameObject goalObject;

    public GameObject fireObject;

    public Rigidbody rb => null;

    public string _prompt;

    public string failPrompt;


    public string InteractionPrompt => _prompt;

    public bool hasInteracted = false;

    public bool Interacted => hasInteracted;

    public Animator popupAnimator;

    public void Interact()
    {
        PopupSystem ppSystem = player.GetComponent<PopupSystem>();

        if (player != null && player.heldItem != null)
        {
            PuzzleItem item = player.heldItem.GetComponent<PuzzleItem>();

            if (item != null)
            {
                Transform targetSlot = null;

                if (player.heldItem.CompareTag(puzzlePieceOneTag))
                {
                    targetSlot = puzzlePieceOneSlot;
                    isPuzzleSlot1Filled = true;
                }
                else if (player.heldItem.CompareTag(puzzlePieceTwoTag))
                {
                    targetSlot = puzzlePieceTwoSlot;
                    isPuzzleSlot2Filled = true;
                }
                if (targetSlot != null)
                {
                    item.transform.localScale = item.originalScale;
                    item.transform.SetParent(targetSlot, false);
                    item.hasInteracted = true;
                    player.heldItem = null;
                }
                else
                {
                    ppSystem.PopUp("There's something else that fits here");
                }
            }

            else
            {
                ppSystem.PopUp(failPrompt);
            }
        }

    }


    private void Update()
    {
        if (isPuzzleSlot1Filled && isPuzzleSlot2Filled)
        {
            goalObject.SetActive(true);
        }
        if (isPuzzleSlot2Filled)
        {
            fireObject.SetActive(true);
        }
    }
}

