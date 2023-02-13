using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorPlayer : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private void Update()
    {
        transform.position = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
    }
}