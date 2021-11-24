using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPiece : MonoBehaviour
{
    //Private Vars
    public int counter;
    private bool isActive;
    private bool isCoroutineActive;

    //Public Vars
    public int timeToMove;
    public float[] anglesToRotate;
    public Quaternion rotation;

    private void Start()
    {
        rotation = transform.localRotation;
        isCoroutineActive = false;
        isActive = false;
        counter = 0;
    }

    //If the player wants to rotate
    private void OnMouseDown()
    {
        if (counter < anglesToRotate.Length && isActive == false)
        {
            StartCoroutine(RotatePiece());
        }
        else if (isActive == false)
        {
            counter = 0;
            StartCoroutine(RotatePiece());
        }
    }

    //Rotate the piece
    private void FixedUpdate()
    {
        if (isActive == true)
        {
            rotation.z = anglesToRotate[counter];
            transform.localRotation = rotation;
        }
    }

    //Make sure the piece can rotate in time
    IEnumerator RotatePiece()
    {
        isCoroutineActive = true;
        isActive = true;
        yield return new WaitForSeconds(timeToMove);
        isActive = false;
        counter += 1;
        isCoroutineActive = false;
    }
}
