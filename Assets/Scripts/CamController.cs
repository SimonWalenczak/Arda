using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] float _rotationX = 0f;
    [SerializeField] float _rotationY = 0f;

    public float sensitivity = 2f;

    [SerializeField] private Transform target;
    [SerializeField] private float _distanceFromTarget = 3.0f;

    private Vector3 _currentRotation;
    private Vector3 smoothVelocity = Vector3.zero;
    
    [SerializeField] float _smoothTime = 0.2f;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        _rotationX += mouseY;
        _rotationY += mouseX;
        _rotationX = Mathf.Clamp(_rotationX, -40, -10);
        _rotationY = Mathf.Clamp(_rotationY, -180, 180);

        Vector3 nextRotation = new Vector3(-_rotationX, _rotationY);
        _currentRotation = Vector3.SmoothDamp(_currentRotation, nextRotation, ref smoothVelocity, _smoothTime);
        transform.localEulerAngles = _currentRotation;

        transform.position = target.position - transform.forward * _distanceFromTarget;
    }
}
