using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedallionDoor : MonoBehaviour
{
    public ObjectTriggerEnter[] triggers;
    public GameObject[] slots;
    public bool[] slotUsed;

    private void Update()
    {
        for (int i = 0; i < 3; i++) //HACK - Fixed at 3
        {
            if (slotUsed[i] == false && triggers[i].snached != null)
            {
                SetObject(triggers[i].snached, slots[i], slotUsed[i]);
            }
        }
    }

    private void SetObject(GameObject objectTo, GameObject slot, bool slotUsed)
    {
        objectTo.transform.parent = slot.transform;
        objectTo.transform.position = slot.transform.position;
        objectTo.transform.rotation = slot.transform.rotation;
        slotUsed = true;
    }
}