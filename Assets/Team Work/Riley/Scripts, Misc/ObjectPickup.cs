using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : ItemEnum
{
    //Private Vars
    private Transform pickupPoint;
    private Rigidbody rigidbody;
    private bool isColliding;
    
    //Public Vars
    [Tooltip("This bool determines if the item is currently picked up")]
    public bool isPickedUp;
    
    [Tooltip("This enum determines what item this is")]
    public IdentityOfObjects identity;

    private void Start()
    {
        isPickedUp = false;
        pickupPoint = GameObject.Find("PickupParent").transform;
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (isPickedUp == true && isColliding == false)
        {
            //rigidbody.MovePosition(Vector3.Lerp(transform.position, pickupPoint.position, Time.deltaTime * 2));
            transform.position = Vector3.Lerp(transform.position, pickupPoint.position, Time.deltaTime * 2);
            float distance = (transform.position - pickupPoint.position).magnitude;
            if (distance < 1)
            {
                transform.position = pickupPoint.position;
            }
        }
    }

    //When the mouse is clicked
    private void OnMouseDown()
    {
        transform.parent = pickupPoint;
        isPickedUp = true;
        rigidbody.useGravity = false;
        transform.position = pickupPoint.position;
    }

    //When the mouse is no longer clicked
    private void OnMouseUp()
    {
        if (isPickedUp == true)
        {
            transform.parent = null;
            rigidbody.useGravity = true;
            isPickedUp = false;
        }
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

    public void FixInPlace()
    {
        rigidbody.isKinematic = true;
    }
}
