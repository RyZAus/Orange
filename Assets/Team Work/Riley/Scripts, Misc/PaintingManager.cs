using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingManager : MonoBehaviour
{
    //Private Vars
    public int counter;
    
    //Public Vars
    [Tooltip("This is the final object to open.")]
    public GameObject drawToOpen;
    [Tooltip("All pieces that are connected to the painting.")]
    public GameObject[] pieces;
    [Tooltip("This should be a number representing which angle should match.")]
    public int[] requiredAngles;

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
            if (currentPiece.counter == requiredAngles[i])
            {
                counter ++;
            }
        }
        if (counter == pieces.Length)
        {
            CompletePuzzle();
        }
        else
        {
            counter = 0;
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
