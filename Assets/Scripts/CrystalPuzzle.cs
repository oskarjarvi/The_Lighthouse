using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalPuzzle : MonoBehaviour
{
    [SerializeField]
    public GameObject lightSource;
    [SerializeField]
    public Material runeMaterial;
 
    private void Update()
    {
        if(lightSource != null && lightSource.activeSelf) 
        {
            runeMaterial.EnableKeyword("_EMISSION");
        }
    }
}
