using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualFunctionPlayAudio : MonoBehaviour
{
    //Private Vars
    private bool isCoRunning = false;
    private AudioSource thisAudioSource;
    
    //Public Vars
    [Tooltip("All audio to be played.")]
    public AudioClip[] audioFirst;
    
    [Tooltip("All audio to be played.")]
    public AudioClip[] audioSecond;
    
    void Start()
    {
        thisAudioSource = GetComponent<AudioSource>();
    }
    
    public void PlayAudioFirst()
    {
        StartCoroutine(PlaySound(audioFirst[Random.Range(0, audioFirst.Length)]));
    }
    
    public void PlayAudioSecond()
    {
        StartCoroutine(PlaySound(audioSecond[Random.Range(0, audioSecond.Length)]));
    }

    IEnumerator PlaySound(AudioClip audioToPlay)
    {
        thisAudioSource.clip = audioToPlay;
        thisAudioSource.Play();
        yield return new WaitForSeconds(.1f);
    }
}
