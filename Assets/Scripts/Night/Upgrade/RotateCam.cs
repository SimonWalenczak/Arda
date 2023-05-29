using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCam : MonoBehaviour
{
    public Transform startRotation;
    public Transform targetRotation;
    public float rotationSpeed = 2f;
    
    private Quaternion initialRotation;
    private Quaternion targetQuaternion;
    
    [SerializeField] private bool IsRotate;
    [SerializeField] private bool IsReturn; 
    
    private Camera cam;
    
    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    
    public void StartRotation()
    {
        IsReturn = false;
        IsRotate = true;
        initialRotation = transform.rotation;
        targetQuaternion = Quaternion.Euler(targetRotation.eulerAngles);
    }
    
    public void ReturnRotation()
    {
        IsReturn = true;
        IsRotate = false;
        initialRotation = transform.rotation;
        targetQuaternion = Quaternion.Euler(startRotation.eulerAngles);
    }
    
    private void Update()
    {
        if (IsRotate && IsReturn == false)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetQuaternion, rotationSpeed * Time.deltaTime);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 78, rotationSpeed * Time.deltaTime);
        }

        if (IsRotate == false && IsReturn == true)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetQuaternion, rotationSpeed * Time.deltaTime);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 60, rotationSpeed * Time.deltaTime);
        }
    }
}