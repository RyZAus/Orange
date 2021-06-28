using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour
{
    //Private Vars
    private NavMeshAgent navMeshRef;
    private bool patrolling;
    private bool playerInVision;
    private bool forgetPlayerRunning;
    private float playerForgetDelay;
    
    //Public Vars
    public float navSafeDistance;
    public GameObject navPatrolPoint;
    public GameObject playerTarget;
    
    void Start()
    {
        patrolling = false;
        forgetPlayerRunning = false;
        if (GetComponent<NavMeshAgent>() != null)
        {
            navMeshRef = GetComponent<NavMeshAgent>();
            navMeshRef.isStopped = true;
        }
    }
    
    void FixedUpdate()
    {
        //Start patrolling if no player around
        if (patrolling != true && playerTarget == null && navMeshRef.isStopped != false)
        {
            ResetNavPath(playerTarget);
            StartNavigation(navPatrolPoint);
        }
        //If player is in sight start navigation
        if (playerTarget != null && navMeshRef.destination != playerTarget.transform.position)
        {
            ResetNavPath(navPatrolPoint);
            StartNavigation(playerTarget);
        }
        //Safe distance for navigation
        if (navMeshRef.remainingDistance < navSafeDistance)
        {
            ResetNavPath(navPatrolPoint);
        }
        //Player target check for navigation stop
        if (playerInVision != true && forgetPlayerRunning != true)
        {
            StartCoroutine(ForgetPlayer());
        }
        else if (playerInVision != false && forgetPlayerRunning == true)
        {
            StopCoroutine(ForgetPlayer());
        }
    }
    
    //Start the nav path
    void StartNavigation(GameObject navLocation)
    {
        patrolling = true;
        navMeshRef.SetDestination(navLocation.transform.position);
        navMeshRef.isStopped = false;
    }

    //Reset the nav path
    void ResetNavPath(GameObject targetToForget)
    {
        patrolling = true;
        navMeshRef.ResetPath();
        navMeshRef.isStopped = true;
        targetToForget = null;
    }
    
    //Forget the player routine
    IEnumerator ForgetPlayer()
    {
        forgetPlayerRunning = true;
        yield return new WaitForSeconds(playerForgetDelay);
        if (playerInVision != true)
        {
            ResetNavPath(playerTarget);
        }
        forgetPlayerRunning = false;
    }
}
