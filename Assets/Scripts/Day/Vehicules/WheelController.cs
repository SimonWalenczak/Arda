using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    [SerializeField] private WheelCollider frontRight;
    [SerializeField] private WheelCollider middleRight;
    [SerializeField] private WheelCollider backRight;
    [SerializeField] private WheelCollider backLeft;    
    [SerializeField] private WheelCollider middleLeft;
    [SerializeField] private WheelCollider frontLeft;
    
    [SerializeField] private Transform frontRightTransform;
    [SerializeField] private Transform middleRightTransform;
    [SerializeField] private Transform backRightTransform;
    [SerializeField] private Transform backLeftTransform;
    [SerializeField] private Transform middleLeftTransform;
    [SerializeField] private Transform frontLeftTransform;

    public float Acceleration = 500f;
    public float BreakingForce = 300f;
    public float maxTurnAngle = 15f;

    private float _currentAcceleration = 0f;
    private float _currentBreakForce = 0f;
    private float _currentTurnAngle = 0f;

    private void FixedUpdate()
    {
        _currentAcceleration = Acceleration * Input.GetAxis("Vertical");
        
        //Frein à main
        if (Input.GetKey(KeyCode.Space))
            _currentBreakForce = BreakingForce;
        else
        {
            _currentBreakForce = 0f;
        }
        frontRight.brakeTorque = _currentBreakForce;
        middleRight.brakeTorque = _currentBreakForce;
        backRight.brakeTorque = _currentBreakForce;
        
        frontLeft.brakeTorque = _currentBreakForce;
        middleLeft.brakeTorque = _currentBreakForce;
        backLeft.brakeTorque = _currentBreakForce;

        //Appliquer les forces
        frontRight.motorTorque = _currentAcceleration;
        frontLeft.motorTorque = _currentAcceleration;
        
        //Pour tourner
        _currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
        frontLeft.steerAngle = _currentTurnAngle;
        frontRight.steerAngle = _currentTurnAngle;
        
            //Visuel (mesh)
        UpdateWheel(frontLeft, frontLeftTransform);
        UpdateWheel(frontRight, frontRightTransform);
        UpdateWheel(middleLeft, middleLeftTransform);
        UpdateWheel(middleRight, middleRightTransform);
        UpdateWheel(backLeft, backLeftTransform);
        UpdateWheel(backRight, backRightTransform);
    }

    void UpdateWheel(WheelCollider col, Transform trans)
    {
        Vector3 position;
        Quaternion rotation;
        col.GetWorldPose(out position, out rotation);

        trans.position = position;
        trans.rotation = rotation;
    }
}
