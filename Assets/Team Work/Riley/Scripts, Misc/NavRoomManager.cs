using System.Collections;
using System.Collections.Generic;
using RileyMcGowan;
using UnityEngine;

public class NavRoomManager : MonoBehaviour
{
    //Private Vars
    private EnemyNav[] enemyNav;
    
    //Public Vars
    public GameObject[] enemies;
    public GameObject[] roomNavPoints;
    
    void Start()
    {
        //On start get all enemynav components 
        for (int i = 0; i < enemies.Length; i++)
        {
            enemyNav[i] = enemies[i].GetComponent<EnemyNav>();
        }
    }

    public void GetNavPoint()
    {
        GameObject chosenNavPoint = roomNavPoints[Random.Range(0, roomNavPoints.Length)];
    }
    
}
