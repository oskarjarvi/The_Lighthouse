using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : InteractableItemBase
{
    private Animator animator;
    public string animationBool;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public override void Interact()
    {
        animator.SetBool(animationBool, true);
    }

}
