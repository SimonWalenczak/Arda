using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMoving : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;

    public List<SoldierState> SoldierStates = new List<SoldierState>();
    private float speed;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        if (animator != null)
        {
            foreach (var item in SoldierStates)
            {
                if (item.isStartAnimation)
                {
                    UpdateState(item.Speed, item.Animation);
                }
            }
        }

    }


    void FixedUpdate()
    {
        MoveSoldier();
    }

    public void UpdateState(float newSpeed, string newAnim)
    {
        float randomStart;
        if (newSpeed!=0)
        {
            randomStart = Random.Range(0.00f, 1.00f);
        }
        else
        {
            randomStart = 0;
        }
        animator.Play("Base Layer." + newAnim, 0, randomStart);
        speed = newSpeed;
    }

    void MoveSoldier()
    {
        rb.MovePosition(transform.position + (transform.forward * speed * Time.deltaTime));
    }
}
