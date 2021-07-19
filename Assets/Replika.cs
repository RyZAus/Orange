using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Replika : MonoBehaviour
{
    public Transform replika;

    private Vector3 lurchRange;
    // Start is called before the first frame update
    void Start()
    {
        lurchRange = new Vector3(Random.Range(-3, 4), 0, Random.Range(-3, 4));
    }

    // Update is called once per frame
    void Update()
    {
        int randomNumber = Random.Range(0, 61);
        if (randomNumber == 1)
        {
            replika.position = Vector3.Lerp(replika.position, replika.position + lurchRange, 1f);
        }
    }
}
