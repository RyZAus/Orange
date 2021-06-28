using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RileyMcGowan
{
    public class EnemyNav : MonoBehaviour
    {
        //Private Vars
        public NavMeshAgent navMeshRef;
        private bool patrolling = false;
        private bool playerInVision;
        private bool forgetPlayerRunning;
        private float playerForgetDelay;
        private NavRoomManager.CurrentRoom pastRoom;
        private bool playerBeingHunted;
        
        //Public Vars
        public float navSafeDistance;
        public GameObject navPatrolPoint;
        public GameObject playerTarget;
        public NavRoomManager navRoomRef;
        public NavRoomManager.CurrentRoom currentRoom;

        void Start()
        {
            playerBeingHunted = false;
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
            //Safe distance for navigation
            if (navMeshRef.remainingDistance < navSafeDistance && patrolling == true)
            {
                ResetNavPath(navPatrolPoint);
            }
            if (navPatrolPoint != null && playerBeingHunted != true && navMeshRef.destination != navPatrolPoint.transform.position)
            {
                ResetNavPath(null);
                StartNavigation(navPatrolPoint);
            }
            
            if (navRoomRef != null && playerTarget == null && navPatrolPoint == null && navMeshRef.isStopped == true)
            {
                navPatrolPoint = navRoomRef.GetNavPoint();
            }
            
            //Start patrolling if no player around
            if (patrolling != true && playerTarget == null && navMeshRef.isStopped == true && navPatrolPoint != null)
            {
                ResetNavPath(playerTarget);
                StartNavigation(navPatrolPoint);
            }

            //If player is in sight start navigation
            if (playerTarget != null && navMeshRef.destination != playerTarget.transform.position)
            {
                ResetNavPath(navPatrolPoint);
                StartNavigation(playerTarget);
                playerBeingHunted = true;
            }
            
            //Player target check for navigation stop
            if (playerInVision != true && forgetPlayerRunning != true && playerTarget != null)
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
            navMeshRef.isStopped = false;
            navMeshRef.SetDestination(navLocation.transform.position);
            patrolling = true;
        }

        //Reset the nav path
        void ResetNavPath(GameObject targetToForget)
        {
            navMeshRef.ResetPath();
            navMeshRef.isStopped = true;
            targetToForget = null;
            patrolling = false;
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
            playerBeingHunted = false;
            forgetPlayerRunning = false;
        }
    }
}