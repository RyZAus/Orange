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
    private DualFunctionPlayAudio audioPlayer;
    
    //Public Vars
    [Tooltip("Standard draws are 0.4")]
    public float drawOpenDistance;
    
    [Tooltip("Is this draw locked by a puzzle?")]
    public bool drawLocked;
    
    private void Start()
    {
        audioPlayer = GetComponent<DualFunctionPlayAudio>();
        placeToStart = transform.localPosition;
        placeToMove = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + drawOpenDistance);
    }

    //Player click to open draw
    private void OnMouseDown()
    {
        if (drawLocked == false)
        {
            
            OpenDrawRun();
        }
    }

    //Puzzles function to open draw rather than player
    public void OpenDrawManual()
    {
        if (drawLocked == true)
        {
            OpenDrawRun();
        }
    }

    //Main draw logic
    private void OpenDrawRun()
    {
        if (isOpen == false)
        {
            audioPlayer.PlayAudioFirst();
            transform.localPosition = placeToMove;
            isOpen = true;
        }
        else if (isOpen == true && drawLocked == false)
        {
            audioPlayer.PlayAudioSecond();
            transform.localPosition = placeToStart;
            isOpen = false;
        }
    }
}
