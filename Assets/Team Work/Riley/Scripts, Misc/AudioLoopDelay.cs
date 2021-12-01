using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoopDelay : MonoBehaviour
{
    //Private Vars
    private bool isCoRunning = false;
    private AudioSource thisAudioSource;
    
    //Public Vars
    [Tooltip("Audio will play when the game starts if true.")]
    public bool playOnStart;
    [Tooltip("Delay between plays of the audio in seconds.")]
    public float waitBetweenPlay;
    
    //Main
    void Start()
    {
        thisAudioSource = GetComponent<AudioSource>();
        if (playOnStart == true)
        {
            StartCoroutine(PlaySound());
        }
    }
    
    void Update()
    {
        if (isCoRunning == false)
        {
            StartCoroutine(PlaySound());
        }
    }

    IEnumerator PlaySound()
    {
        isCoRunning = true;
        thisAudioSource.Play();
        yield return new WaitForSeconds(thisAudioSource.clip.length + waitBetweenPlay);
        isCoRunning = false;
    }
}
