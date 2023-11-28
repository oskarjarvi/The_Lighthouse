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

    private AudioSource _audioSource;

    public AudioClip _audioClip;

    public float delay = 1f;


    public void Awake()
    {
        animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();


    }
    public void Interact()
    {
        if(_interactable && controller != null)
        {
            controller.StartPuzzle();
            controller.SequenceTrigger(this);
            animator.SetBool(animationBool, true);

            

            _interactable = false;
        }
    }
    public void Reset()
    {
        animator.SetBool(animationBool, false);
        _interactable = true;

    }
    private void PlaySound()
    {
        _audioSource.clip = _audioClip;
        _audioSource.Play();
    }
    private void StopSound()
    {
        _audioSource.Stop();
    }

}
