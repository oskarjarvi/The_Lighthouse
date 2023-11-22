using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour, InteractableItemBase
{
    private Animator animator;
    public string animationBool;
    [SerializeField]
    private string _prompt;



    public Rigidbody rb => null;

    public bool hasInteracted = false;
    public string InteractionPrompt => _prompt;

    public bool Interacted => hasInteracted;

    

    private void Awake()
    {
        animator = GetComponent<Animator>();
       
    }
    public void Interact()
    {
        hasInteracted = true;
        animator.SetBool(animationBool, true);
    }
    

}
