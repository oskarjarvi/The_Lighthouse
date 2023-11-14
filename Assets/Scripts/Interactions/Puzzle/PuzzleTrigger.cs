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

    private Animator animator;

    public string animationBool;

    public Rigidbody rb => null;

    public string InteractionPrompt => _prompt;

    public bool Interacted => !_interactable;

    public void Awake()
    {
        animator = GetComponent<Animator>();

    }
    public void Interact()
    {
        if(_interactable)
        {
            controller.StartPuzzle();
            controller.SequenceTrigger(this);

            _interactable = false;
            animator.SetBool(animationBool, true);

        }
    }
    public void Reset()
    {
        _interactable = true;
        animator.SetBool(animationBool, false);
    }
}
