using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public bool hasPiece;
    public int slot;
    public GameObject portalPairObject;
    void Start()
    {
        slot = GetComponentInParent<SlidingGridPiece>().slotNumber;
        portalPairObject = GetComponentInParent<SlidingGridPiece>().portalPair;
        hasPiece = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasPiece)
        {
            portalPairObject.SetActive(true);
            return;
        }
        portalPairObject.SetActive(false);
        
    }
}
