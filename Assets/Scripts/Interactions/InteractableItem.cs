using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour, InteractableItemBase
{
    private Animator animator;
    public string animationBool;
    [SerializeField]
    private string _prompt;
    [SerializeField]
    private string _failPrompt;

    private bool isAnimationPaused = false;

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
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DoorBlocker"))
        {
            Debug.Log("Collision with DoorBlocker detected");

            // Pause the animation by setting the speed to 0
            if (!isAnimationPaused)
            {
                animator.speed = 0f;
                isAnimationPaused = true;
            }
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DoorBlocker"))
        {
            Debug.Log("Exited collision with DoorBlocker");

            // Resume the animation by setting the speed back to 1
            if (isAnimationPaused)
            {
                animator.speed = 1f;
                isAnimationPaused = false;
            }
        }
    }



   


}
