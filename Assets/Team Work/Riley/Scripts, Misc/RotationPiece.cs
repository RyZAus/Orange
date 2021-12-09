using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPiece : MonoBehaviour
{
    //Private Vars
    private bool isActive;
    private bool isCoroutineActive;
    private PlayAudioOnFunction audioPlayer;

    //Public Vars
    public int counter;
    public int timeToMove;
    public float[] anglesToRotate;
    public Vector3 thisRotation;
    public event Action rotatedEvent;
    
    private void Start()
    {
        audioPlayer = GetComponent<PlayAudioOnFunction>();
        isCoroutineActive = false;
        isActive = false;
        counter = 0;
        thisRotation = transform.rotation.eulerAngles;
    }
            //Notice how we don't read every frame
            
    //If the player wants to rotate
    private void OnMouseDown()
    {
        if (counter < anglesToRotate.Length && isActive == false)
        {
            audioPlayer.PlayAudio();
            StartCoroutine(RotatePiece());
        }
        else if (isActive == false)
        {
            audioPlayer.PlayAudio();
            counter = 0;
            StartCoroutine(RotatePiece());
        }
    }

    //Rotate the piece
    private void FixedUpdate()
    {
        if (isActive == true)
        {
            thisRotation.z = anglesToRotate[counter];
            transform.rotation = Quaternion.Euler(thisRotation);
            //transform.rotation = Quaternion.Slerp(transform.rotation, thisRotation, Time.deltaTime * timeToMove / 2);
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
        rotatedEvent?.Invoke();
        isCoroutineActive = false;
    }
}
