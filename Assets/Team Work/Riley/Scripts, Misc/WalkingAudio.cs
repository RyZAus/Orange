using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WalkingAudio : MonoBehaviour
{
    //Private Vars
    private bool isCoRunning;
    private int currentRange;
    private int previousRange;
    private Vector3 lastPosition = new Vector3(0,0,0);
    
    //Public Vars
    [Tooltip("This is the delay between individual steps.")]
    public float stepInterval;
    [Tooltip("The speaker for steps to come from.")]
    public AudioSource footSpeaker;
    [Tooltip("All possible step variations.")]
    public AudioClip[] stepSounds;

    private void Start()
    {
        isCoRunning = false;
    }

    private void Update()
    {
        if (isCoRunning == false)
        {
            if (lastPosition != transform.position)
            {
                StartCoroutine(IsWalking());
            }
            lastPosition = transform.position;
        }
        else if (isCoRunning == true)
        {
            if (lastPosition == transform.position)
            {
                StopCoroutine(IsWalking());
                footSpeaker.Stop();
            }
            lastPosition = transform.position;
        }
    }

    IEnumerator IsWalking()
    {
        isCoRunning = true;
        currentRange = Random.Range(0, stepSounds.Length);
        if (currentRange != previousRange)
        {
            footSpeaker.clip = stepSounds[currentRange];
        }
        else
        {
            currentRange = Random.Range(0, stepSounds.Length);
            footSpeaker.clip = stepSounds[currentRange];
        }
        footSpeaker.Play();
        yield return new WaitForSeconds(stepInterval);
        isCoRunning = false;
    }
}