using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Replika : MonoBehaviour
{
    public Transform replika;

    private Vector3 lurchRange;
    private GameManager _gameManager;
    private Vector3 startPos;
    public float lurchAmount;
    bool disabled;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.paused)
        {
            disabled = true;
        }

        if (!GameManager.paused)
        {
            disabled = false;
        }

        if (disabled)
        {
            return;
        }
        int randomNumber = Random.Range(0, 11);
        if (randomNumber == 0)
        {
            StartCoroutine(ReplikaLerch());
        }
    }

    IEnumerator ReplikaLerch()
    {
        lurchRange = new Vector3(Random.Range(-lurchAmount, lurchAmount), 0, Random.Range(-lurchAmount, lurchAmount));
        startPos = replika.position;
        replika.position = Vector3.Lerp(replika.position, replika.position + lurchRange, 1f);
        yield return new WaitForEndOfFrame();
        replika.position = startPos;
        Debug.Log("replika moved");
    }
}