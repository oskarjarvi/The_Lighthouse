using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableItemBase : MonoBehaviour
{
    protected Rigidbody rb;
    public abstract void Interact();

}
