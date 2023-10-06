using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Transform prefab;
    private bool _isPickedUp = false;
    public bool IsPickedUp { get { return _isPickedUp; } set { _isPickedUp = value; } }
    [SerializeField]
    public bool isInspectable;

    public Rigidbody rb;
    public Collider itemCollider;

}
