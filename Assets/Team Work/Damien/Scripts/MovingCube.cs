using System.Collections;
using System.Collections.Generic;
using Tanks;
using UnityEngine;

namespace Damien
{
    public class MovingCube : MonoBehaviour
    {
        private GameObject go;
        private Vector3 currentPos;
        private Vector3 startPos;
        private Rigidbody rb;
        public int forceAmount;
        public int timeBetween;
        public bool randomMovement;


        // Start is called before the first frame update
        void Start()
        {
            go = gameObject;
            rb = go.GetComponent<Rigidbody>();
            startPos = transform.position;
            switch (randomMovement)
            {
                case true:
                    StartCoroutine(MoveRandomly());
                    break;
                case false:
                   StartCoroutine(MoveBackAndForth());
                   break;
                    
            }
            
        }

        // Update is called once per frame
        void Update()
        {
            
            
           
        }

        IEnumerator MoveRandomly()
        {
            
        }

        IEnumerator MoveBackAndForth()
        {
            while (true)
            {
                rb.AddForce(0,0,forceAmount);
                yield return new WaitForSeconds(timeBetween);
                rb.AddForce(0, 0, -forceAmount);
                rb.AddForce(0, 0, -forceAmount);
                yield return new WaitForSeconds(timeBetween);
                rb.AddForce(0,0,forceAmount);
            }
        }
    }

}