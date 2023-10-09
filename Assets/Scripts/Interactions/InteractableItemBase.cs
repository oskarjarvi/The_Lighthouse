using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableItemBase : MonoBehaviour, IInteractable
{
    protected Rigidbody rb;
    public abstract void Interact();
   
}
