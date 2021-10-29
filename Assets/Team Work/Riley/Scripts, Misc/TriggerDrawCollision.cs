using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDrawCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ItemEnum>() != null)
        {
            GameObject itemGO = other.gameObject;
            itemGO.transform.parent = transform;
            itemGO.transform.rotation = transform.rotation;
        }
    }
}
