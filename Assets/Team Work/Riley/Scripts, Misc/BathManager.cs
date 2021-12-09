using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathManager : MonoBehaviour
{
    //Private Vars
    private Vector3 waterStartingLevel;
    
    //Public Vars
    public float counter;
    public GameObject waterLevel;
    [Tooltip("This is the final object to open.")]
    public GameObject drawToOpen;
    [Tooltip("All pieces that are connected to the painting.")]
    public GameObject[] pieces;
    [Tooltip("This should be a number representing which angle should match.")]
    public int[] requiredAngles;

    private void Start()
    {
        waterStartingLevel = waterLevel.transform.position;
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
                if (counter <= pieces.Length)
                {
                    waterLevel.transform.position = new Vector3(waterStartingLevel.x, waterStartingLevel.y + (counter/2), waterStartingLevel.z);
                }
                else
                {
                    waterLevel.transform.position = waterStartingLevel;
                }
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
