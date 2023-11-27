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

    private bool hasPrompted = false;


    public GameObject goalObject;

    public GameObject fireObject;

    public Rigidbody rb => null;

    public string _prompt;

    public string failPrompt;
    private PopupSystem ppSystem;



    public string InteractionPrompt => _prompt;

    public bool hasInteracted = false;

    public bool Interacted => hasInteracted;

    public Animator popupAnimator;

    private void Awake()
    {
        ppSystem = player.GetComponent<PopupSystem>();
    }
    public void Interact()
    {

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
                else
                {
                    ppSystem.PopUp("There's something else that fits here");
                }
                if (targetSlot != null)
                {
                    item.transform.localScale = item.originalScale;
                    item.transform.SetParent(targetSlot, false);
                    item.hasInteracted = true;
                    player.heldItem = null;
                }
            }

            else
            {
                ppSystem.PopUp(failPrompt);
            }
        }
        else
        {
            ppSystem.PopUp("It looks like some items are missing");
        }

    }


    private void Update()
    {
        if (isPuzzleSlot1Filled && isPuzzleSlot2Filled && !hasPrompted)
        {
            goalObject.SetActive(true);
            ppSystem.PopUp("It lit up! I wonder where it leads? I should head down and investigate");
            hasPrompted = true;
        }
        if (isPuzzleSlot2Filled)
        {
            fireObject.SetActive(true);
        }
    }
}

