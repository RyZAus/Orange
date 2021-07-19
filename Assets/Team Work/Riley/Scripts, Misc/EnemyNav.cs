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
        public NavMeshAgent navMeshRef;
        private bool patrolling = false;
        private bool playerInVision = false;
        private bool forgetPlayerRunning;
        private float playerForgetDelay;
        private NavRoomManager.CurrentRoom pastRoom;
        private bool playerBeingHunted;
        private Damien.FOV thisFOV;
        
        //Public Vars
        public float navSafeDistance;
        public GameObject navPatrolPoint;
        public GameObject playerTarget;
        public NavRoomManager navRoomRef;
        public NavRoomManager.CurrentRoom currentRoom;

        void Start()
        {
            //Set Private Funcs
            playerBeingHunted = false;
            patrolling = false;
            forgetPlayerRunning = false;
            //Get Components
            if (GetComponent<NavMeshAgent>() != null)
            {
                navMeshRef = GetComponent<NavMeshAgent>();
                navMeshRef.isStopped = true;
            }
            if (GetComponent<Damien.FOV>() != null)
            {
                thisFOV = GetComponent<Damien.FOV>();
            }
            //Delegate State / Manager
            findNavPoint.Enter = StartNavFind;
            findNavPoint.Update = UpdateNavFind;
            findNavPoint.Exit = EndNavFind;
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

        void FixedUpdate()
        {
            currentStateManager.UpdateState();
            
            if (thisFOV.listOfTargets.Count > 0 && playerTarget == null)
            {
                playerInVision = true;
                playerTarget = thisFOV.listOfTargets[0];
            }
            else if (playerInVision != false)
            {
                playerInVision = false;
            }
        }
        
        private void StartNavFind(){}

        private void UpdateNavFind()
        {
            if (navRoomRef != null && playerTarget == null && navPatrolPoint == null && navMeshRef.isStopped == true)
            {
                navPatrolPoint = navRoomRef.GetNavPoint();
                if (playerInVision == false)
                {
                    currentStateManager.ChangeState(navToPoint);
                }
            }
        }
        private void EndNavFind(){}

        private void StartNavToPoint()
        {
            //Start patrolling if no player around
            if (navPatrolPoint != null && patrolling != true && playerTarget == null && navMeshRef.isStopped == true)
            {
                ResetNavPath(playerTarget);
                StartNavigation(navPatrolPoint);
            }
        }

        private void UpdateNavToPoint()
        {
            if (playerInVision == true)
            {
                currentStateManager.ChangeState(navToPlayer);
            }
            if (navPatrolPoint != null && playerBeingHunted != true && navMeshRef.destination != navPatrolPoint.transform.position)
            {
                ResetNavPath(null);
                StartNavigation(navPatrolPoint);
            }
        }
        private void EndNavToPoint(){}
        private void StartNavToPlayer(){}

        private void UpdateNavToPlayer()
        {
            if (playerBeingHunted == false && playerTarget == null && playerInVision == false)
            {
                currentStateManager.ChangeState(findNavPoint);
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
            //If player is in sight start navigation
            if (playerTarget != null && navMeshRef.destination != playerTarget.transform.position)
            {
                ResetNavPath(navPatrolPoint);
                StartNavigation(playerTarget);
                playerBeingHunted = true;
            }
        }
        private void EndNavToPlayer(){}
        private void StartAttackPlayer(){}
        private void UpdateAttackPlayer(){}
        private void EndAttackPlayer(){}
        
        

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
                playerTarget = null;
            }
            playerBeingHunted = false;
            forgetPlayerRunning = false;
        }
    }
}