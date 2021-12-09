using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeOutside : MonoBehaviour
{
    public GameObject blockout;
    public WalkingAudio walkingAudio;
    public AudioSource audioPlay;
    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(EndGame());
        }
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(3);
        walkingAudio.enabled = false;
        blockout.SetActive(true);
        yield return new WaitForSeconds(1);
        audioPlay.Play();
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        
    }
}
