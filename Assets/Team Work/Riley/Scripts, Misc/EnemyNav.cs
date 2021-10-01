using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RileyMcGowan
{
    public class EnemyNav : MonoBehaviour
    {
        //Delegate State / Manager
        public DelegateStateManager currentStateManager = new DelegateStateManager();
        public DelegateState findNavPoint = new DelegateState();
        public DelegateState navToPoint = new DelegateState();
        public DelegateState navToPlayer = new DelegateState();
        public DelegateState attackPlayer = new DelegateState();

        //Private Vars
        private NavMeshAgent navMeshRef;
        private Damien.FOV enemyFOV;
        private NavRoomManager navRoomRef;
        private GameObject patrolTarget;
        private GameObject playerTarget;
        private GameObject playerTargetLost;
        public bool playerTargetActive;
        public bool patrolTargetActive;
        private Vector3 targetLocation;
        private Vector3 lastTargetLocation;
        
        //Public Vars
        public List<GameObject> possibleNavPoints;
        public float safeDistance = 5;
        
        private void Start()
        {
            //Set Default Vars
            playerTargetActive = false;
            patrolTargetActive = false;
            //Grab References
            if (GetComponent<Damien.FOV>() != null)
            {
                enemyFOV = GetComponent<Damien.FOV>();
            }
            if (GetComponent<NavMeshAgent>() != null)
            {
                navMeshRef = GetComponent<NavMeshAgent>();
            }
            if (FindObjectOfType<NavRoomManager>() != null)
            {
                navRoomRef = FindObjectOfType<NavRoomManager>();
            }
            else
            {
                Debug.Log("Enemy cannot nav, needs NavRoomManager.");
                return;
            }
            //Set state defaults
            findNavPoint.Enter = StartFindNavPoint;
            findNavPoint.Update = UpdateFindNavPoint;
            findNavPoint.Exit = EndFindNavPoint;
            navToPoint.Enter = StartNavToPoint;
            navToPoint.Update = UpdateNavToPoint;
            navToPoint.Exit = EndNavToPoint;
            navToPlayer.Enter = StartNavToPlayer;
            navToPlayer.Update = UpdateNavToPlayer;
            navToPlayer.Exit = EndNavToPlayer;
            attackPlayer.Enter = StartAttackPlayer;
            attackPlayer.Update = UpdateAttackPlayer;
            attackPlayer.Exit = EndAttackPlayer;
            //Swap state
            currentStateManager.ChangeState(findNavPoint);
        }

        private void FixedUpdate()
        {
            //Make the states update
            currentStateManager.UpdateState();
            //If the FOV has a target and isn't already active
            if (enemyFOV.listOfTargets.Count > 0)
            {
                playerTarget = enemyFOV.listOfTargets[0];
                lastTargetLocation = playerTarget.transform.position;
            }
            if (playerTargetActive == false && enemyFOV.listOfTargets.Count > 0)
            {
                //Set the target
                playerTarget = enemyFOV.listOfTargets[0];
                //Now it's active
                playerTargetActive = true;
            }
            //If we have no target but it's still active
            else if(enemyFOV.listOfTargets.Count == 0 && playerTargetActive == true)
            {
                //Then set the lost target
                playerTargetLost = playerTarget;
            }
        }

        /// <summary>
        /// Find a nav location
        /// </summary>
        private void StartFindNavPoint()
        {
            if (patrolTargetActive == false)
            {
                ResetNavPath();
                playerTargetActive = false;
                patrolTarget = navRoomRef.GetNavPoint();
                patrolTargetActive = true;
            }
        }

        private void UpdateFindNavPoint()
        {
            if (playerTargetActive == true)
            {
                patrolTargetActive = false;
            }
            else
            {
                if (patrolTargetActive == true && playerTargetActive == false)
                {
                    currentStateManager.ChangeState(navToPoint);
                }
                else if (patrolTargetActive == false && playerTargetActive == false)
                {
                    patrolTarget = navRoomRef.GetNavPoint();
                    patrolTargetActive = true;
                }
            }
        }

        private void EndFindNavPoint(){}
        
        /// <summary>
        /// Move to location
        /// </summary>
        private void StartNavToPoint()
        {
            StartNavigation(patrolTarget.transform.position);
        }

        private void UpdateNavToPoint()
        {
            if (playerTargetActive == true)
            {
                patrolTargetActive = false;
                currentStateManager.ChangeState(navToPlayer);
            }
            else
            {
                CheckNavigation(patrolTargetActive);
                if (patrolTargetActive == false && playerTargetActive == false)
                {
                    patrolTargetActive = false;
                    currentStateManager.ChangeState(findNavPoint);
                }
            }
        }

        private void EndNavToPoint(){}

        /// <summary>
        /// Move to player
        /// </summary>
        private void StartNavToPlayer()
        {
            StartNavigation(playerTarget.transform.position);
        }

        private void UpdateNavToPlayer()
        {
            if (navMeshRef.remainingDistance < safeDistance)
            {
                currentStateManager.ChangeState(attackPlayer);
            }
            else if (playerTargetActive == false)
            {
                ResetNavPath();
                currentStateManager.ChangeState(findNavPoint);
            }
            else
            {
                if (lastTargetLocation != targetLocation)
                {
                    ResetNavPath();
                    StartNavigation(playerTarget.transform.position);
                }
                else
                {
                    if (playerTargetActive == true)
                    {
                        CheckNavigation(playerTargetActive);
                    }
                }
            }
        }

        private void EndNavToPlayer()
        {
            
        }

        /// <summary>
        /// Attack the player
        /// </summary>
        private void StartAttackPlayer()
        {
            //Dabladge
        }

        private void UpdateAttackPlayer()
        {
            StartCoroutine(WaitToEndPlayer());
        }

        private void EndAttackPlayer()
        {
            
        }
        
        //Start the nav path
        private void StartNavigation(Vector3 navLocation)
        {
            targetLocation = navLocation;
            navMeshRef.SetDestination(targetLocation);
        }

        //Reset the nav path
        private void ResetNavPath()
        {
            navMeshRef.ResetPath();
        }

        private void CheckNavigation(bool targetActive)
        {
            if (navMeshRef.remainingDistance < safeDistance || navMeshRef.remainingDistance == null)
            {
                ResetNavPath();
                if (targetActive == playerTargetActive)
                {
                    playerTargetActive = false;
                }
                if (targetActive == patrolTargetActive)
                {
                    patrolTargetActive = false;
                }
            }
        }

        IEnumerator WaitToEndPlayer()
        {
            yield return new WaitForSeconds(10);
            playerTargetActive = false;
            currentStateManager.ChangeState(findNavPoint);
        }
    }
}