using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrigger : InteractableItemBase
{
    public int triggerIndex;
    public SequencePuzzleController controller;
    private bool _interactable = true;

    public Animator animator;
    public AnimationClip animationClip;

    public override void Interact()
    {
        Debug.Log(_interactable);
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
