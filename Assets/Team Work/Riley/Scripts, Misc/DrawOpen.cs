using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawOpen : MonoBehaviour
{
    private bool isOpen = false;
    private Vector3 placeToStart;
    private Vector3 placeToMove;

    private void Start()
    {
        placeToStart = transform.position;
        placeToMove = new Vector3(transform.position.x, transform.position.y, transform.position.z + .4f);
    }

    private void OnMouseDown()
    {
        if (isOpen == false)
        {
            transform.position = placeToMove;
            isOpen = true;
        }
        else if (isOpen == true)
        {
            transform.position = placeToStart;
            isOpen = false;
        }
    }
}
