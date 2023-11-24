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

    public GameObject goalObject;

    public Rigidbody rb => null;

    public string _prompt;

    public string failPrompt;


    public string InteractionPrompt => _prompt;

    public bool hasInteracted = false;

    public bool Interacted => hasInteracted;

    public Animator popupAnimator;

    public void Interact()
    {
        if(player != null && player.heldItem != null) 
        {
            if (player.heldItem.CompareTag(puzzlePieceOneTag))
            {
                PuzzleItem item = player.heldItem.GetComponent<PuzzleItem>();
                if (item != null)
                {
                    item.transform.SetParent(puzzlePieceOneSlot, false);
                    item.hasInteracted = true;
                    player.heldItem = null;
                }
            }
            else if( player.heldItem.CompareTag(puzzlePieceTwoTag))
            {
                PuzzleItem item = player.heldItem.GetComponent<PuzzleItem>();
                if (item != null)
                {
                    item.transform.SetParent(puzzlePieceTwoSlot, false);
                    item.hasInteracted = true;
                    player.heldItem = null;
                }
            }
            else
            {
                Debug.Log("You're using the wrong items bro");
            }
        }
        
    }
    private void Update()
    {
        if(puzzlePieceOneSlot != null && puzzlePieceTwoSlot != null ) 
        {
            
            goalObject.SetActive(true);
        }
    }
}

