using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cadran : MonoBehaviour
{
    public Transform playerTransform;
    private void Update()
    {
        Vector3 rotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(rotation.x, rotation.y, playerTransform.rotation.eulerAngles.y);
    }
}
