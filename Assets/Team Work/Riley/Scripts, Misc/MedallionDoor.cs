using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedallionDoor : MonoBehaviour
{
    //Private Vars
    private AudioSource audioPlayer;

    //Public Vars
    public AudioClip[] medallionSounds;
    public ObjectTriggerEnter[] triggers;
    public GameObject[] slots;
    public bool[] slotsUsed;

    public GameObject[] doorToMove;

    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    private void Update()
    {
        for (int i = 0; i < 3; i++) //HACK - Fixed at 3
        {
            if (slotsUsed[i] == false && triggers[i].snached != null)
            {
                SetObject(triggers[i].snached, slots[i], i);
            }
        }
    }

    private void SetObject(GameObject objectTo, GameObject slot, int boolIteration)
    {
        objectTo.transform.parent = slot.transform;
        objectTo.transform.position = slot.transform.position;
        objectTo.transform.rotation = slot.transform.rotation;
        slotsUsed[boolIteration] = true;
        int counter = 0;
        for (int i = 0; i < slotsUsed.Length; i++)
        {
            if (slotsUsed[i] == true)
            {
                counter += 1;
            }
            if (counter == slotsUsed.Length)
            {
                for (int j = 0; j < doorToMove.Length; j++)
                {
                    StartCoroutine(OpenMainDoor(j));
                }
            }
            else if (counter == 2)
            {
                audioPlayer.clip = medallionSounds[1];
                audioPlayer.Play();
            }
            else if (counter == 1)
            {
                audioPlayer.clip = medallionSounds[0];
                audioPlayer.Play();
            }
        }
    }

    IEnumerator OpenMainDoor(int j)
    {
        audioPlayer.clip = medallionSounds[2];
        audioPlayer.Play();
        yield return new WaitForSeconds(medallionSounds[2].length);
        Vector3 transformUnder = new Vector3(doorToMove[j].transform.position.x, doorToMove[j].transform.position.y - 10, doorToMove[j].transform.position.z);
        doorToMove[j].GetComponent<PlayAudioOnFunction>().PlayAudio();
        doorToMove[j].transform.position = transformUnder;
    }
}