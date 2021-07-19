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
        private GameObject patrolTarget;
        private GameObject playerTarget;
        private Damien.FOV enemyFOV;
        
        //Public Vars
        public List<GameObject> possibleNavPoints;

        private void Start()
        {
            if (GetComponent<Damien.FOV>() != null)
            {
                enemyFOV = GetComponent<Damien.FOV>();
            }
            currentStateManager.ChangeState(findNavPoint);
        }

        private void FixedUpdate()
        {
            currentStateManager.UpdateState();
            if (enemyFOV.listOfTargets.Count > 0)
            {
                playerTarget = enemyFOV.listOfTargets[0]; //HACK FOR 1 PLAYER
            }
        }

        /// <summary>
        /// Find a nav location
        /// </summary>
        private void StartNavFind()
        {
            
        }

        private void UpdateNavFind()
        {
            
        }

        private void EndNavFind()
        {
            
        }
        
        /// <summary>
        /// Move to location
        /// </summary>
        private void StartNavToPoint()
        {
            
        }

        private void UpdateNavToPoint()
        {
            
        }

        private void EndNavToPoint()
        {
            
        }

        /// <summary>
        /// Move to player
        /// </summary>
        private void StartNavToPlayer()
        {
            
        }

        private void UpdateNavToPlayer()
        {
            
        }

        private void EndNavToPlayer()
        {
            
        }

        /// <summary>
        /// Attack the player
        /// </summary>
        private void StartAttackPlayer()
        {
            
        }

        private void UpdateAttackPlayer()
        {
            
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
    }
}