using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeSpawner : MonoBehaviour
{
    //Array to store : All objects to spawn
    public GameObject[] objectsToSpawn;
    
    //Array to store : All spawn locations
    public GameObject[] locationsToSpawn;

    //Array to store : Spawned objects
    public GameObject[] objectsSpawned;

    //Check if the IEnumerator is running
    private bool isRunning;
    
    private void Start()
    {
        objectsSpawned = new GameObject[locationsToSpawn.Length];
        isRunning = false;
        //For loop to spawn as many objects as there are locations to spawn
        for (int i = 0; i < locationsToSpawn.Length; i++)
        {
            //Take the gameObjects array and choose randomly one of the objects in it
            GameObject currentSpawnable = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
            //You can then Instantiate the object on the spawn location using the "i" from the for loop
            GameObject spawnedObject = Instantiate(currentSpawnable, locationsToSpawn[i].transform.position, Quaternion.identity);
            //Store all spawned objects so we can use them later?
            objectsSpawned[i] = spawnedObject;
        }

        if (isRunning != true)
        {
            StartCoroutine(UnloadAllSpawned());
        }
    }

    IEnumerator UnloadAllSpawned()
    {
        isRunning = true;
        //Wait for a few seconds
        yield return new WaitForSeconds(10);
        //For every spawned objects
        for (int i = 0; i < objectsSpawned.Length; i++)
        {
            //Remove them from the scene
            Destroy(objectsSpawned[i]);
        }
        isRunning = false;
    }
}
