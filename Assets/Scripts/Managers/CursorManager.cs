using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public event Action<bool> OnCursorStateChanged;

    private void Awake()
    {
        // Initial state (unlocked by default)
        SetCursorState(true);
    }

    public void SetCursorState(bool isUnlocked)
    {
        Cursor.lockState = isUnlocked ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isUnlocked;

        // Notify subscribers about the cursor state change
        OnCursorStateChanged?.Invoke(isUnlocked);
    }
   
}
