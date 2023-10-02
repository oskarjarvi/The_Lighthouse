using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Transform prefab;
    public bool isPickedUp { get; set; }
    [SerializeField]
    public bool isInspectable;
    
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        isPickedUp = false;
    }



}
