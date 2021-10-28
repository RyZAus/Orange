using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTriggerEnter : ItemEnum
{
    public IdentityOfObjects slotRequirement;
    public GameObject snached;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ObjectPickup>() != null)
        {
            if (other.GetComponent<ObjectPickup>().identity == slotRequirement)
            {
                snached = other.gameObject;
            }
        }
    }
}
