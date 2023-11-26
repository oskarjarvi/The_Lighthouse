using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{

    private void Awake()
    {
        SetCursorState(true);
    }

    public void SetCursorState(bool isUnlocked)
    {
        Cursor.lockState = isUnlocked ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isUnlocked;

       
    }
   
}
