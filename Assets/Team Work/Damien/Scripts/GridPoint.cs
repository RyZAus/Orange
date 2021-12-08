using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public bool hasPiece;
    public int slot;
    void Start()
    {
        slot = GetComponentInParent<SlidingGridPiece>().slotNumber;
        hasPiece = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
