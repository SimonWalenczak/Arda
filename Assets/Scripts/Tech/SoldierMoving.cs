using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMoving : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;
    [SerializeField] float speed;
    [SerializeField] bool fastRun;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        if (fastRun)
        {
            animator.SetBool("fastrun", true);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveSoldier();
    }

    void MoveSoldier()
    {
        //Vector3 tempVect = new Vector3(0, 0, 0);
        //tempVect = tempVect.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + (transform.forward * speed * Time.deltaTime));
    }
}
