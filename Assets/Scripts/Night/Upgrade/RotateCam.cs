using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCam : MonoBehaviour
{
    public Transform targetRotation;
    public float rotationSpeed = 2f;

    private Quaternion initialRotation;
    private Quaternion targetQuaternion;

    private bool IsRotate;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    public void StartRotation()
    {
        IsRotate = true;
        initialRotation = transform.rotation;
        targetQuaternion = Quaternion.Euler(targetRotation.eulerAngles);
    }

    private void Update()
    {
        if (IsRotate)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetQuaternion, rotationSpeed * Time.deltaTime);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 78, rotationSpeed * Time.deltaTime);
        }
    }
}