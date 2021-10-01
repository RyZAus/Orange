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

        //Private Vars
        private NavMeshAgent navMeshRef;
        private Damien.FOV enemyFOV;
        private NavRoomManager navRoomRef;
        private GameObject patrolTarget;
        private GameObject playerTarget;
        private Vector3 locationCheck;
        private Vector3 playerPreviousLocation;
        private bool hasPreviousLocation;
        public bool isDaTingStopped;
        
        //Public Vars
        public List<GameObject> possibleNavPoints;
        public float safeDistance = 5;
        
        private void Start()
        {
            //Default Vars
            hasPreviousLocation = false;
            isDaTingStopped = true;
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
            //If the FOV has a target
            if (playerTarget == null && enemyFOV.listOfTargets.Count > 0)
            {
                //Playertarget is now relevant
                playerTarget = enemyFOV.listOfTargets[0]; //HACK FOR 1 PLAYER
                //Nav to player
                currentStateManager.ChangeState(navToPlayer);
            }
            else
            {
                //If we have a player target set the previous location
                if (playerTarget != null && enemyFOV.listOfTargets.Count < 1 && isDaTingStopped == true)
                {
                    playerPreviousLocation = playerTarget.transform.position;
                    hasPreviousLocation = true;
                    //Then set player target to null
                    playerTarget = null;
                }
            }
        }

        /// <summary>
        /// Find a nav location
        /// </summary>
        private void StartFindNavPoint()
        {
            //Reset the patrol target and stop nav
            ResetNavPath(patrolTarget);
            //Get a new navpoint
            patrolTarget = navRoomRef.GetNavPoint();
            //Set the players previous location
            hasPreviousLocation = false;
        }

        private void UpdateFindNavPoint()
        {
            if (patrolTarget != null && playerTarget == null)
            {
                currentStateManager.ChangeState(navToPoint);
            }
            else if (patrolTarget == null && playerTarget == null)
            {
                patrolTarget = navRoomRef.GetNavPoint();
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
            if (playerTarget == null && patrolTarget == null && isDaTingStopped == true)
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
            //Starts nav to player
            StartNavigation(playerTarget.transform.position);
        }

        private void UpdateNavToPlayer()
        {
            if (navMeshRef.isStopped == true && playerTarget != null)
            {
                StartNavigation(playerTarget.transform.position);
            }
            //If I don't have a player
            if (playerTarget == null)
            {
                //Reset everything
                ResetNavPath();
                StartNavigation(playerPreviousLocation);
                currentStateManager.ChangeState(findNavPoint);
            }
            else
            {
                CheckNavigation(playerTarget, true);
            }
            if (isDaTingStopped == true && hasPreviousLocation == false)
            {
                Debug.Log(navMeshRef.isStopped);
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
            isDaTingStopped = false;
            navMeshRef.SetDestination(navLocation);
        }

        //Reset the nav path
        void ResetNavPath(GameObject targetToForget = default(GameObject))
        {
            isDaTingStopped = true;
            targetToForget = null;
            navMeshRef.ResetPath();
        }
        void ResetNavPathNoBool(GameObject targetToForget = default(GameObject))
        {
            targetToForget = null;
            navMeshRef.ResetPath();
        }
        
        //Check nav location
        void CheckNavigation(GameObject navLocation, bool noBool = default(bool))
        {
            if (navMeshRef.remainingDistance < safeDistance || playerTarget != null && patrolTarget != null)
            {
                if (navMeshRef.remainingDistance < safeDistance)
                {
                    patrolTarget = null;
                }
                if (playerTarget == null)
                {
                    if (noBool != true)
                    {
                        ResetNavPath(navLocation);
                    }
                    else
                    {
                        ResetNavPathNoBool(navLocation);
                    }
                }
                else
                {
                    if (noBool != true)
                    {
                        ResetNavPath();
                    }
                    else
                    {
                        ResetNavPathNoBool();
                    }
                }
            }
            else
            {
                locationCheck = new Vector3(navLocation.transform.position.x, navMeshRef.destination.y, navLocation.transform.position.z);
                if (locationCheck != navMeshRef.destination)
                {
                    if (noBool != true)
                    {
                        ResetNavPath();
                    }
                    else
                    {
                        ResetNavPathNoBool();
                    }
                    StartNavigation(navLocation.transform.position);
                }
            }
        }
    }
}