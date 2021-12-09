using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingGridPiece : MonoBehaviour
{
    public GameObject gridPoint;

    public Vector3 gridPointLoc;
    // Start is called before the first frame update
    public int slotNumber;
    public GameObject portalPair;
    
    void Start()
    {
        gridPoint = GetComponentInChildren<GridPoint>().gameObject;
        gridPointLoc = gridPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
