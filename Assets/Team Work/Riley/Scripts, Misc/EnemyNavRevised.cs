using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RileyMcGowan
{
    public class EnemyNavRevised : MonoBehaviour
    {
        //Delegate State / Manager
        public DelegateStateManager currentStateManager = new DelegateStateManager();
        public DelegateState findNavPoint = new DelegateState();
        public DelegateState navToPoint = new DelegateState();
        public DelegateState navToPlayer = new DelegateState();
        public DelegateState attackPlayer = new DelegateState();
        public float safeDistance = 5;

        //Private Vars
        private NavMeshAgent navMeshRef;
        private GameObject patrolTarget;
        private GameObject playerTarget;
        private Damien.FOV enemyFOV;
        private NavRoomManager navRoomRef;
        private Vector3 locationCheck;
        private Vector3 playerPreviousLocation;
        
        //Public Vars
        public List<GameObject> possibleNavPoints;
        
        private void Start()
        {
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
            currentStateManager.ChangeState(findNavPoint);
        }

        private void FixedUpdate()
        {
            currentStateManager.UpdateState();
            if (enemyFOV.listOfTargets.Count > 0)
            {
                playerTarget = enemyFOV.listOfTargets[0]; //HACK FOR 1 PLAYER
                currentStateManager.ChangeState(navToPlayer);
            }
            else
            {
                if (playerTarget != null)
                {
                    playerPreviousLocation = playerTarget.transform.position;
                }
                playerTarget = null;
            }
        }

        /// <summary>
        /// Find a nav location
        /// </summary>
        private void StartFindNavPoint()
        {
            ResetNavPath(patrolTarget);
            patrolTarget = navRoomRef.GetNavPoint();
            playerPreviousLocation = Vector3.zero;
        }

        private void UpdateFindNavPoint()
        {
            if (patrolTarget != null && playerTarget == null)
            {
                currentStateManager.ChangeState(navToPoint);
            }
        }

        private void EndFindNavPoint()
        {
            
        }
        
        /// <summary>
        /// Move to location
        /// </summary>
        private void StartNavToPoint()
        {
            StartNavigation(patrolTarget.transform.position);
        }

        private void UpdateNavToPoint()
        {
            if (playerTarget == null && patrolTarget == null && navMeshRef.isStopped == true)
            {
                currentStateManager.ChangeState(findNavPoint);
            }
            CheckNavigation(patrolTarget);
        }

        private void EndNavToPoint()
        {
            
        }

        /// <summary>
        /// Move to player
        /// </summary>
        private void StartNavToPlayer()
        {
            StartNavigation(playerTarget.transform.position);
        }

        private void UpdateNavToPlayer()
        {
            if (playerTarget == null)
            {
                ResetNavPath();
                StartNavigation(playerPreviousLocation);
                currentStateManager.ChangeState(findNavPoint);
            }
            else
            {
                CheckNavigation(playerTarget);
            }
            if (navMeshRef.isStopped == true && playerPreviousLocation == Vector3.zero)
            {
                currentStateManager.ChangeState(attackPlayer);
            }
        }

        private void EndNavToPlayer()
        {
            ResetNavPath();
        }

        /// <summary>
        /// Attack the player
        /// </summary>
        private void StartAttackPlayer()
        {
            //DEAL DAMAGE
        }

        private void UpdateAttackPlayer()
        {
            currentStateManager.ChangeState(findNavPoint);
        }

        private void EndAttackPlayer()
        {
            
        }
        
        //Start the nav path
        void StartNavigation(Vector3 navLocation)
        {
            navMeshRef.isStopped = false;
            navMeshRef.SetDestination(navLocation);
        }

        //Reset the nav path
        void ResetNavPath(GameObject targetToForget = default(GameObject))
        {
            navMeshRef.ResetPath();
            navMeshRef.isStopped = true;
            targetToForget = null;
        }
        
        //Check nav location
        void CheckNavigation(GameObject navLocation)
        {
            if (navMeshRef.remainingDistance < safeDistance || playerTarget != null && patrolTarget != null)
            {
                if (playerTarget == null)
                {
                    ResetNavPath(navLocation);
                }
                else
                {
                    ResetNavPath();
                }
            }
            else
            {
                locationCheck = new Vector3(navLocation.transform.position.x, navMeshRef.destination.y, navLocation.transform.position.z);
                if (locationCheck != navMeshRef.destination)
                {
                    ResetNavPath();
                    StartNavigation(navLocation.transform.position);
                }
            }
        }
    }
}