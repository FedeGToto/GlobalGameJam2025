using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }

    public virtual void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
}
