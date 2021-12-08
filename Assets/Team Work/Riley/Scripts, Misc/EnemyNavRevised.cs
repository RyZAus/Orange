using System;
using System.Collections;
using System.Collections.Generic;
using Damien;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace RileyMcGowan
{
    public class EnemyNavRevised : MonoBehaviour
    {
        //Delegate State / Manager
        public DelegateStateManager currentState = new DelegateStateManager();
        public DelegateState buffer = new DelegateState();
        public DelegateState navigate = new DelegateState();
        public DelegateState attack = new DelegateState();

        //Private Vars
        private FOV enemyFOV;
        private IEnumerator currentCo;
        private NavMeshAgent navMeshRef;
        private bool doWeHavePlayer = false;
        private bool blind = false;
        private GameObject navigationPoint;
        private Vector3 pastNavigationPoint;
        private Animator thisAnimator;

        //Public Vars
        public GameObject[] navigationPoints;
        public float playerLossTime;
        public float safeDistance = 3;

        private void Start()
        {
            //Grab References
            if (GetComponent<Damien.FOV>() != null)
            {
                enemyFOV = GetComponent<Damien.FOV>();
            }

            if (GetComponent<NavMeshAgent>() != null)
            {
                navMeshRef = GetComponent<NavMeshAgent>();
            }
            
            if (GetComponent<Animator>() != null)
            {
                thisAnimator = GetComponent<Animator>();
            }
            currentCo = DelayCounter();
            
            //Set state defaults
            buffer.Enter = Buffer_Enter;
            buffer.Update = Buffer_Update;
            buffer.Exit = Buffer_Exit;
            navigate.Enter = Navigate_Enter;
            navigate.Update = Navigate_Update;
            navigate.Exit = Navigate_Exit;
            attack.Enter = Attack_Enter;
            attack.Update = Attack_Update;
            attack.Exit = Attack_Exit;

            //Swap current state
            currentState.ChangeState(buffer);
        }

        private void Update()
        {
            //Update the update for states
            currentState.UpdateState();
            if (doWeHavePlayer == false && enemyFOV.listOfTargets.Count > 0 && blind == false)
            {
                doWeHavePlayer = true;
                navigationPoint = enemyFOV.listOfTargets[0];
            }
            else if (doWeHavePlayer == true && enemyFOV.listOfTargets.Count > 0)
            {
                ResetDelayCounter();
            }
        }

        /// <summary>
        /// Find Nav Point
        /// </summary>
        private void Buffer_Enter() {}

        private void Buffer_Update()
        {
            //Where do we want the AI to chase
            if (doWeHavePlayer == false)
            {
                currentState.ChangeState(navigate);
            }
            else if (doWeHavePlayer == true)
            {
                currentState.ChangeState(attack);
            }
        }

        private void Buffer_Exit() {}

        /// <summary>
        /// Start Navigating
        /// </summary>
        private void Navigate_Enter()
        {
            thisAnimator.SetTrigger("Run");
            navigationPoint = navigationPoints[Random.Range(0, navigationPoints.Length)];
            navMeshRef.SetDestination(navigationPoint.transform.position);
        }

        private void Navigate_Update()
        {
            if (navMeshRef.remainingDistance <= safeDistance || doWeHavePlayer == true)
            {
                thisAnimator.SetTrigger("Stop");
                navMeshRef.ResetPath();
                currentState.ChangeState(buffer);
            }
        }

        private void Navigate_Exit() {}

        /// <summary>
        /// Chase and attack the player
        /// </summary>
        private void Attack_Enter()
        {
            thisAnimator.SetTrigger("Chase");
            pastNavigationPoint = navigationPoint.transform.position;
            navMeshRef.SetDestination(navigationPoint.transform.position);
            StartCoroutine(currentCo);
        }

        private void Attack_Update()
        {
            if (doWeHavePlayer == false)
            {
                thisAnimator.SetTrigger("Stop");
                navMeshRef.ResetPath();
                currentState.ChangeState(buffer);
            }
            else if (navMeshRef.remainingDistance <= safeDistance)
            {
                AttackThePlayer();
                navMeshRef.ResetPath();
                currentState.ChangeState(buffer);
            }
            else if (pastNavigationPoint != navigationPoint.transform.position && doWeHavePlayer == true)
            {
                navMeshRef.ResetPath();
                pastNavigationPoint = navigationPoint.transform.position;
                navMeshRef.SetDestination(navigationPoint.transform.position);
            }
        }

        private void Attack_Exit()
        {
            StartCoroutine(BlindDelay());
            doWeHavePlayer = false;
        }
        
        private void AttackThePlayer()
        {
            //Attack the player visuals
            thisAnimator.SetTrigger("Attack");
            if (navigationPoint.GetComponent<CameraAttackEffect>() != null)
            {
                navigationPoint.GetComponent<CameraAttackEffect>().AttackPlayer();
            }
        }

        private void ResetDelayCounter()
        {
            StopCoroutine(currentCo);
            currentCo = DelayCounter();
            StartCoroutine(currentCo);
        }
        
        IEnumerator BlindDelay()
        {
            blind = true;
            yield return new WaitForSeconds(playerLossTime);
            blind = false;
        }

        IEnumerator DelayCounter()
        {
            yield return new WaitForSeconds(playerLossTime);
            doWeHavePlayer = false;
        }
    }
}