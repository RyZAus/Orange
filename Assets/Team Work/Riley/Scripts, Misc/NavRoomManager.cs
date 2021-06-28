using System;
using System.Collections;
using System.Collections.Generic;
using RileyMcGowan;
using UnityEngine;
using Random = UnityEngine.Random;

public class NavRoomManager : MonoBehaviour
{
    //Private Vars
    private EnemyNav enemyNav;
    
    //Public Vars
    public GameObject[] roomNavPoints;
    public CurrentRoom managersRoom;
    
    public enum CurrentRoom
    {
        None,
        MainRoom,
        F1R1, //Floor 1, Room 1
        F2R1
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            enemyNav = other.gameObject.GetComponent<EnemyNav>();
            enemyNav.currentRoom = managersRoom;
            enemyNav.navRoomRef = this.gameObject.GetComponent<NavRoomManager>();
        }
    }

    public GameObject GetNavPoint()
    {
        GameObject chosenNavPoint = roomNavPoints[Random.Range(0, roomNavPoints.Length)];
        return chosenNavPoint;
    }
    
}
