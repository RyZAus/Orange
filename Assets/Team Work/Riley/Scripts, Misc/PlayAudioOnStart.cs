using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnStart : MonoBehaviour
{
    //Private Vars
    private AudioSource thisAudioSource;
    
    //Public Vars
    public AudioClip audioToPlay;
    
    void Start()
    {
        thisAudioSource = GetComponent<AudioSource>();
        PlaySound();
    }

    private void PlaySound()
    {
        thisAudioSource.clip = audioToPlay;
        thisAudioSource.Play();
    }
}
