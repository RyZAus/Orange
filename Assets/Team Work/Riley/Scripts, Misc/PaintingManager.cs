using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingManager : MonoBehaviour
{
    //Private Vars
    
    //Public Vars
    public GameObject storedObject;
    public GameObject snapPoint;
    public ObjectTriggerEnter activeScript;
    private void FixedUpdate()
    {
        if (activeScript.snached != null)
        {
            //Make the object move correctly
            storedObject = activeScript.snached;
            storedObject.transform.position = snapPoint.transform.position;
            storedObject.transform.rotation = snapPoint.transform.rotation;
            storedObject.transform.parent = snapPoint.transform;
            //Remove it from active
            activeScript.snached = null;
        }
    }
}
