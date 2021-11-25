using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingManager : MonoBehaviour
{
    //Private Vars
    public int counter;
    
    //Public Vars
    public GameObject drawToOpen;
    public GameObject[] pieces;
    public float[] requiredAngles;

    private void Start()
    {
        counter = 0;
        foreach (GameObject currentPiece in pieces)
        {
            if (currentPiece.GetComponent<RotationPiece>() != null)
            {
                currentPiece.GetComponent<RotationPiece>().rotatedEvent += CheckRotations;
            }
            else
            {
                Debug.LogError(currentPiece + " is not a rotation piece!");
            }
        }
    }

    private void CheckRotations()
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            RotationPiece currentPiece = pieces[i].GetComponent<RotationPiece>();
            if (currentPiece.anglesToRotate[currentPiece.counter] == requiredAngles[i])
            {
                counter ++;
            }
        }
        if (counter == pieces.Length)
        {
            CompletePuzzle();
        }
    }

    private void CompletePuzzle()
    {
        if (drawToOpen != null)
        {
            drawToOpen.GetComponent<DrawOpen>().OpenDrawManual();
        }
        else
        {
            Debug.LogError("Final draw not assigned.");
        }
    }
}
