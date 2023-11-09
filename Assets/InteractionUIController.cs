using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject _uiPanel;
    [SerializeField]
    private TextMeshProUGUI _promptText;

    public bool IsDisplayed = false;


    // Start is called before the first frame update
    void Start()
    {
        _uiPanel.SetActive(false);
    }

    // Update is called once per frame
    
    public void SetUp(string promptText)
    {
        _promptText.text = promptText;
        _uiPanel.SetActive(true);
        IsDisplayed = true;

    }
    public void Close()
    {
        _uiPanel.SetActive(false);
        IsDisplayed=false;

    }
}
