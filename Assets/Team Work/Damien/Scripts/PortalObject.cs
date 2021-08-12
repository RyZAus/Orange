using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalObject : PortalTraveller
{
    public float yaw;
    public float pitch;
    float smoothYaw;
    float smoothPitch;
    
    float yawSmoothV;
    float pitchSmoothV;
    float verticalVelocity;
    Vector3 velocity;
    Vector3 smoothV;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        yaw = transform.eulerAngles.y;
        pitch = transform.localEulerAngles.x;
        smoothYaw = yaw;
        smoothPitch = pitch;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Teleport(Transform fromPortal, Transform toPortal, Vector3 pos, Quaternion rot)
    {
        transform.position = pos;
        Vector3 eulerRot = rot.eulerAngles;
        float delta = Mathf.DeltaAngle(smoothYaw, eulerRot.y);
        yaw += delta;
        smoothYaw += delta;
        transform.eulerAngles = Vector3.up * smoothYaw;
        velocity = toPortal.TransformVector(fromPortal.InverseTransformVector(velocity));
        Physics.SyncTransforms();
    }
}
