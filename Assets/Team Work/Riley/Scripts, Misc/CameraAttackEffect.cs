using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class CameraAttackEffect : MonoBehaviour
{
    //Private Vars
    private bool isRunning;
    private HDAdditionalCameraData thisCameraData;
    private float originalAperture;
    private float timeThen;
    
    //Public Vars
    public float attackAperture = 30;

    private void Start()
    {
        thisCameraData = GetComponentInChildren<HDAdditionalCameraData>();
        originalAperture = thisCameraData.physicalParameters.aperture;
    }

    private void Update()
    {
        if (isRunning == true)
        {
            float thisAperture = thisCameraData.physicalParameters.aperture;
            if (thisAperture > originalAperture)
            {
                thisCameraData.physicalParameters.aperture = Mathf.Lerp(attackAperture, originalAperture, (Time.time - timeThen) * .05f);
            }
            else if (thisAperture <= originalAperture)
            {
                thisCameraData.physicalParameters.aperture = originalAperture;
                isRunning = false;
            }
        }
    }

    public void AttackPlayer()
    {
        thisCameraData.physicalParameters.aperture = attackAperture;
        isRunning = true;
        timeThen = Time.time;
    }
}