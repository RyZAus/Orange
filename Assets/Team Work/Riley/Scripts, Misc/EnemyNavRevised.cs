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
        
        //Public Vars
        public List<GameObject> possibleNavPoints;
        
        private void Start()
        {
            if (GetComponent<Damien.FOV>() != null)
            {
                enemyFOV = GetComponent<Damien.FOV>();
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
                playerTarget = null;
            }
        }

        /// <summary>
        /// Find a nav location
        /// </summary>
        private void StartFindNavPoint()
        {
            patrolTarget = navRoomRef.GetNavPoint();
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
            StartNavigation(patrolTarget);
        }

        private void UpdateNavToPoint()
        {
            if (playerTarget == null && patrolTarget == null)
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
            StartNavigation(playerTarget);
        }

        private void UpdateNavToPlayer()
        {
            if (navMeshRef.isStopped == true)
            {
                currentStateManager.ChangeState(attackPlayer);
            }
            CheckNavigation(playerTarget);
        }

        private void EndNavToPlayer()
        {
            
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
        void StartNavigation(GameObject navLocation)
        {
            navMeshRef.isStopped = false;
            navMeshRef.SetDestination(navLocation.transform.position);
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
                if (navLocation.transform.position != navMeshRef.destination)
                {
                    ResetNavPath();
                    StartNavigation(navLocation);
                }
            }
        }
    }
}