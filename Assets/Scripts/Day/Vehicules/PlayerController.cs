using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float w_speed, wb_speed, olw_speed, rn_speed, ro_speed;
    [SerializeField] private Transform playerTrans;

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _rigidbody.velocity = transform.forward * w_speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            _rigidbody.velocity = -transform.forward * wb_speed * Time.deltaTime;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            _animator.SetTrigger("Weak");
        }
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _animator.SetTrigger("Walk");
            _animator.ResetTrigger("Idle");
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            _animator.ResetTrigger("Walk");
            _animator.SetTrigger("Idle");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _animator.SetTrigger("WalkBack");
            _animator.ResetTrigger("Idle");
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            _animator.ResetTrigger("WalkBack");
            _animator.SetTrigger("Idle");
        }

        if (Input.GetKey(KeyCode.Q))
        {
            playerTrans.Rotate(0, -ro_speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerTrans.Rotate(0, ro_speed * Time.deltaTime, 0);
        }
    }
}
