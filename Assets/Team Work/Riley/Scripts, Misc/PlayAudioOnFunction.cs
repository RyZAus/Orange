using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnFunction : MonoBehaviour
{
    //Private Vars
    private bool isCoRunning = false;
    private AudioSource thisAudioSource;
    
    //Public Vars
    [Tooltip("Delay between plays of the audio in seconds.")]
    public float waitBetweenPlay;
    
    [Tooltip("All audio to be played.")]
    public AudioClip[] audioToPlay;
    
    void Start()
    {
        thisAudioSource = GetComponent<AudioSource>();
    }
    
    public void PlayAudio()
    {
        if (isCoRunning == false)
        {
            StartCoroutine(PlaySound());
        }
    }

    IEnumerator PlaySound()
    {
        isCoRunning = true;
        thisAudioSource.clip = audioToPlay[Random.Range(0, audioToPlay.Length)];
        thisAudioSource.Play();
        yield return new WaitForSeconds(thisAudioSource.clip.length + waitBetweenPlay);
        isCoRunning = false;
    }
}
