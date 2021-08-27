using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Damien
{
    public class PlayerFOV : MonoBehaviour
    {
        public LayerMask targets;
        private Vector3 playerPos;

        public float viewRadius = 30f;
        [Range(0, 360)] public float viewAngle = 90f;
        
        


        public List<GameObject> listOfTargets = new List<GameObject>();

        private void Start()
        {
            StartCoroutine("SeeThings", 0.1f);
        }

        private void Update()
        {
            playerPos = transform.position;
        }

        IEnumerator SeeThings(float delay)
        {
            while (true)
            {
                yield return new WaitForSeconds(delay);
                DetectThings();
            }
        }

        void DetectThings()
        {
                //clears the list of targets ready to scan the area again
                
                //Collects all colliders that have the layermask marked as a target
                listOfTargets.Clear();
                Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targets);
                for (int i = 0; i < targetsInViewRadius.Length; i++)
                {
                    Collider target = targetsInViewRadius[i];
                    Vector3 targetPos = target.transform.position;
                    //checks if the target that is inside the view range is also within the view cone
                    Vector3 dirToTarget = (targetPos - transform.position).normalized; 
                    Vector3 topHit = new Vector3(targetPos.x, targetPos.y + 1f, targetPos.z);
                    Vector3 leftHit = new Vector3(targetPos.x, targetPos.y + 1.5f, targetPos.z - .75f);
                    Vector3 rightHit = new Vector3(targetPos.x, targetPos.y + 1.5f, targetPos.z + .75f);
                   
                    if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2) 
                    {
                        float distance = Vector3.Distance(playerPos, target.transform.position);
                        //Debug.Log(distance);
                        RaycastHit hit1;
                        //checks if there is anything blocking the line of sight to the target
                        Physics.Raycast(playerPos, dirToTarget, out hit1, distance);
                        Debug.DrawRay(playerPos, dirToTarget, Color.magenta, 1);
                        
                            
                        if (hit1.collider == targetsInViewRadius[i])
                        {
                           Debug.DrawRay(transform.position, dirToTarget, Color.green, 1);
                            //adds the target to the list of valid Targets
                            listOfTargets.Add(target.gameObject);
                        }
                        
                    }
                }
        }/* Handles.DrawLine(fov.transform.position, new Vector3(targetPos.x + .25f, targetPos.y, targetPos.z));
            Handles.DrawLine(fov.transform.position, new Vector3(targetPos.x, targetPos.y + 3f, targetPos.z));
            Handles.DrawLine(fov.transform.position, new Vector3(targetPos.x, targetPos.y + 1.5f, targetPos.z -1f));
            Handles.DrawLine(fov.transform.position, new Vector3(targetPos.x, targetPos.y + 1.5f, targetPos.z + 1));*/

        public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += transform.eulerAngles.y;
            }
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}