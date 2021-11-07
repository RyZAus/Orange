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
        private Vector3 playerPosOffsetL;
        private Vector3 playerPosOffsetR;
        private Vector3 playerLocalPos;
        public float offset;
        private bool renderPortal;
        private Camera playerCamera;

        public float viewRadius = 30f;
        [Range(0, 360)] public float viewAngle = 90f;
        
        


        public List<GameObject> listOfTargets = new List<GameObject>();

        private void Start()
        {
            playerCamera = GetComponentInChildren<Camera>();
            StartCoroutine("SeeThings", 0.1f);
        }

        private void Update()
        {
            playerPos = transform.position;
            playerLocalPos = playerCamera.transform.position;
            
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
                listOfTargets.Clear();
                renderPortal = false;
                Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targets);
                for (int i = 0; i < targetsInViewRadius.Length; i++)
                {
                    Collider target = targetsInViewRadius[i];
                    Vector3 targetPos = target.transform.position;
                    //checks if the target that is inside the view range is also within the view cone
                    Vector3 dirToTarget = (targetPos - transform.position).normalized;

                    if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2) 
                    {
                        float distance = Vector3.Distance(playerPos, target.transform.position);
                        //Debug.Log(distance);
                        RaycastHit middleHit;
                        RaycastHit leftHit;
                        RaycastHit rightHit;
                        //checks if there is anything blocking the line of sight to the target
                        Physics.Raycast(playerPos, dirToTarget, out middleHit, distance);
                        Debug.DrawRay(playerPos, dirToTarget, Color.magenta, 1);
                        Physics.Raycast(playerPosOffsetL, dirToTarget, out leftHit, distance);
                        Debug.DrawRay(playerPosOffsetL, dirToTarget, Color.magenta, 1);
                        Physics.Raycast(playerPosOffsetR, dirToTarget, out rightHit, distance);
                        Debug.DrawRay(playerPosOffsetR, dirToTarget, Color.magenta, 1);
                        
                        if (middleHit.collider == targetsInViewRadius[i])
                        {
                            Debug.DrawRay(transform.position, dirToTarget, Color.green, 1);
                            //adds the target to the list of valid Targets
                            //listOfTargets.Add(target.gameObject);
                            renderPortal = true;
                        }
                        if (leftHit.collider == targetsInViewRadius[i])
                        {
                            Debug.DrawRay(playerPosOffsetL, dirToTarget, Color.green, 1);
                            //adds the target to the list of valid Targets
                            //listOfTargets.Add(target.gameObject);
                            renderPortal = true;

                        }
                        if (rightHit.collider == targetsInViewRadius[i])
                        {
                           Debug.DrawRay(playerPosOffsetR, dirToTarget, Color.green, 1);
                            //adds the target to the list of valid Targets
                            //listOfTargets.Add(target.gameObject);
                            renderPortal = true;
                        }

                        if (renderPortal)
                        {
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