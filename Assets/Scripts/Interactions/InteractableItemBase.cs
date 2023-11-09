using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable enable

public interface InteractableItemBase
{
    public Rigidbody? rb { get; }
    public void Interact();

    public string InteractionPrompt { get; }

    public bool Interacted { get; }

}
