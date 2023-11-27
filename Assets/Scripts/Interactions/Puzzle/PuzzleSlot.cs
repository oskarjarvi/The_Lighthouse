using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using TMPro;

public class PuzzleSlot : MonoBehaviour, InteractableItemBase
{
    [SerializeField]
    private string _prompt;
    [SerializeField]
    private string failPrompt;
    [SerializeField]
    private string wrongItemPrompt;

    public string expectedItemTag;

    public Player player;
    public Transform expectedItemSlot;
    private Animator animator;
    public string animatorBool;

    public Rigidbody rb => null;

    public string InteractionPrompt => _prompt;

    public bool hasInteracted = false;

    public bool Interacted => hasInteracted;

    private AudioSource audioSource;




    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }
    public void Interact()
    {
        PopupSystem ppSystem = player.GetComponent<PopupSystem>();

        if (player.heldItem != null)
        {

            if (player.heldItem.CompareTag(expectedItemTag))
            {

                PuzzleItem item = player.heldItem.GetComponent<PuzzleItem>();

                item.hasInteracted = true;

                item.transform.SetParent(expectedItemSlot, false);


                player.heldItem = null;

                //Trigger animation
                if (animator != null && audioSource != null)
                {
                    animator.SetBool(animatorBool, true);                    

                }
            }
            else
            {
                ppSystem.PopUp(wrongItemPrompt);

            }
        }

        else
        {
            //trigger false animation/sound
            ppSystem.PopUp(failPrompt);
        }

    }
    private void PlaySound()
    {
        audioSource.Play();
    }
    private void StopSound()
    {
        audioSource.Stop();
    }


}
