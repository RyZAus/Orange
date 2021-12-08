using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPuzzleManager : MonoBehaviour
{
    public GridPoint[] grid;
    
    // Start is called before the first frame update
    void Start()
    {
        grid = gameObject.GetComponentsInChildren<GridPoint>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
