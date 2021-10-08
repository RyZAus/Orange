using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    //Place for the item to go
    private Transform pickupPoint;
    //Rigidbody
    private Rigidbody rigidbody;
    //Is Picked Up
    private bool isPickedUp;
    //Is Picked Up
    private bool isColliding;
    //Time
    private float time;
    
    private void Start()
    {
        isPickedUp = false;
        pickupPoint = GameObject.Find("PickupParent").transform;
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //If I have picked up this object and if it is in this players possession
        if (isPickedUp == true && transform.parent == pickupPoint)
        {
            //Move it towards the position it was at
            if (isColliding == false)
            {
                //rigidbody.MovePosition(Vector3.Lerp(transform.position, pickupPoint.position, Time.deltaTime * 2));
                transform.position = Vector3.Lerp(transform.position, pickupPoint.position, Time.deltaTime * 2);
                float distance = (transform.position - pickupPoint.position).magnitude;
                if (distance < .5)
                {
                    transform.position = pickupPoint.position;
                }
            }
        }
    }

    //When the mouse is clicked
    private void OnMouseDown()
    {
        //Bool to check if we picked it up
        isPickedUp = true;
        //Remove gravity
        rigidbody.useGravity = false;
        //Set it's position
        transform.position = pickupPoint.position;
        //Keep it at that position
        transform.parent = pickupPoint;
    }

    //When the mouse is no longer clicked
    private void OnMouseUp()
    {
        if (transform.parent == pickupPoint)
        {
            //Disconnect the object
            transform.parent = null;
            //Enable gravity
            rigidbody.useGravity = true;
        }
        //Bool to check if we dropped it
        isPickedUp = false;
    }

    private void OnCollisionStay(Collision other)
    {
        if (isColliding == false)
        {
            isColliding = true;
        }
    }
    
    private void OnCollisionExit(Collision other)
    {
        isColliding = false;
    }
}
