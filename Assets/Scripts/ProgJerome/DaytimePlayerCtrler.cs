using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DaytimePlayerCtrler : MonoBehaviour
{
    [HideInInspector] public ArcadeCar arcadeCar;
    [HideInInspector] public bool isDriving = true;


    void Start()
    {
        arcadeCar = GetComponent<ArcadeCar>();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "TentTrigger")
        {
            if (Gamepad.current.buttonSouth.isPressed && isDriving)
            {
                isDriving = false;
                arcadeCar.controllable = false;
                other.gameObject.GetComponent<Tent>().GoToTent();
                //print("coucou c'est une tente");            
            }
        }
    }
}
