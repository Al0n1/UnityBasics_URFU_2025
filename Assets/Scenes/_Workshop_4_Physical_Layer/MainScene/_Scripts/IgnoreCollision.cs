using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    public Collider colliderSphere1;
    public Collider colliderSphere2;

    void Start()
    {
        Physics.IgnoreCollision(colliderSphere1, colliderSphere2);
    }
}
