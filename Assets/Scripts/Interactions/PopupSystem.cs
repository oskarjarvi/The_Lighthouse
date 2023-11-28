using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupSystem : MonoBehaviour
{

    public Animator popupAnimator;
    public TMP_Text popUpText;
    public GameObject popUpBox;

    public float delay = 1f;



    public void PopUp(string _feedBackPrompt)
    {
        popUpBox.SetActive(true);
        popUpText.text = _feedBackPrompt;

        popupAnimator.SetTrigger("Show");

        Invoke("ClosePopUp", delay);

    }
    private void ClosePopUp()
    {
        popupAnimator.SetTrigger("Hide");

        Invoke("DeactivatePopupBox", delay);
    }
    private void DeactivatePopupBox()
    {
        popUpBox.SetActive(false);
    }
}
