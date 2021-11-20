using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPiece : MonoBehaviour
{
    //Private Vars
    private int counter;
    private bool isActive;
    private bool isCoroutineActive;

    //Public Vars
    public int timeToMove;
    public float[] anglesToRotate;
    public directionToRotate thisDirection;

    public enum directionToRotate
    {
        x,
        y,
        z
    }

    private void Start()
    {
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
            if (thisDirection == directionToRotate.x)
            {
                Vector3 rotation = new Vector3(anglesToRotate[counter], 0, 0);
                transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, rotation, Time.deltaTime * timeToMove / 2);
            }
            if (thisDirection == directionToRotate.y)
            {
                Vector3 rotation = new Vector3(0, anglesToRotate[counter], 0);
                transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, rotation, Time.deltaTime * timeToMove / 2);
            }
            if (thisDirection == directionToRotate.z)
            {
                Vector3 rotation = new Vector3(0, 0, anglesToRotate[counter]);
                transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, rotation, Time.deltaTime * timeToMove / 2);
            }
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
