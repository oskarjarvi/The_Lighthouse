using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrigger :MonoBehaviour, InteractableItemBase
{
    [SerializeField]
    private string _prompt;

    public int triggerIndex;
    public SequencePuzzleController controller;
    private bool _interactable = true;

    public Animator animator;
    public AnimationClip animationClip;

    public Rigidbody rb => null;

    public string InteractionPrompt => _prompt;

    public bool Interacted => !_interactable;

    public void Interact()
    {
        if(_interactable)
        {
            controller.StartPuzzle();
            controller.SequenceTrigger(this);

            //triggerAnimation
            _interactable = false;
        }
    }
    public void Reset()
    {
        Debug.Log("resetting this variable");
        _interactable = true;
        //Inverse animation
    }
}
