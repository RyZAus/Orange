using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingPiece : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject go;
    private Vector3 pos;
    private ObjectPickup objPU;
    private float objHeight;
    private Vector3 plusHieght;
    private Rigidbody rb;
    private GridPuzzleManager manager;
    void Start()
    {
        manager = FindObjectOfType<GridPuzzleManager>();
        go = gameObject;
        objPU = go.GetComponent<ObjectPickup>();
        rb = go.GetComponent<Rigidbody>();
        objHeight = go.transform.localScale.y;
        plusHieght = new Vector3(0, objHeight / 2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<GridPoint>())
        {
            foreach (var point in manager.grid)
            {
                point.hasPiece = false;
            }
            GridPoint gp = other.GetComponent<GridPoint>();
            var gpTransform = gp.transform;
            //rb.isKinematic = true;
            go.transform.position = gpTransform.position + plusHieght;
            go.transform.rotation = gpTransform.rotation;
            gp.hasPiece = true;
            
        }
    }
}
