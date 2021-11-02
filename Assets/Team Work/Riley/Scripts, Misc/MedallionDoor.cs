using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedallionDoor : MonoBehaviour
{
    public ObjectTriggerEnter[] triggers;
    public GameObject[] slots;
    public bool[] slotsUsed;

    private void Update()
    {
        for (int i = 0; i < 3; i++) //HACK - Fixed at 3
        {
            if (slotsUsed[i] == false && triggers[i].snached != null)
            {
                SetObject(triggers[i].snached, slots[i], i);
            }
        }
    }

    private void SetObject(GameObject objectTo, GameObject slot, int boolIteration)
    {
        objectTo.transform.parent = slot.transform;
        objectTo.transform.position = slot.transform.position;
        objectTo.transform.rotation = slot.transform.rotation;
        slotsUsed[boolIteration] = true;
        int counter = 0;
        for (int i = 0; i < slotsUsed.Length; i++)
        {
            if (slotsUsed[i] == true)
            {
                counter += 1;
            }
            if (counter == slotsUsed.Length)
            {
                Vector3 transformUnder = new Vector3(transform.position.x, transform.position.y - 10, transform.position.z);
                transform.position = transformUnder;
            }
        }
    }
}