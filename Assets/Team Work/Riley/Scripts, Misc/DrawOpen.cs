using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawOpen : MonoBehaviour
{
    //Private Vars
    private bool isOpen = false;
    private Vector3 placeToStart;
    private Vector3 placeToMove;
    
    //Public Vars
    [Tooltip("Standard draws are 0.4")]
    public float drawOpenDistance;

    private void Start()
    {
        placeToStart = transform.localPosition;
        placeToMove = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + drawOpenDistance);
    }

    private void OnMouseDown()
    {
        if (isOpen == false)
        {
            transform.localPosition = placeToMove;
            isOpen = true;
        }
        else if (isOpen == true)
        {
            transform.localPosition = placeToStart;
            isOpen = false;
        }
    }
}
