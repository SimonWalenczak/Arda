using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DaytimePlayerCtrler : MonoBehaviour
{
    [HideInInspector] public ArcadeCar arcadeCar;
    [HideInInspector] public bool isDriving = true;
    
    public GameObject AButtonDebug;

    void Start()
    {
        arcadeCar = GetComponent<ArcadeCar>();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "TentTrigger")
        {
            if(isDriving)
                AButtonDebug.SetActive(true);
            
            if (Gamepad.current.buttonSouth.isPressed && isDriving)
            {
                if (other.gameObject.GetComponent<Tent>().Enterable)
                {
                    AButtonDebug.SetActive(false);

                    isDriving = false;
                    arcadeCar.controllable = false;
                    other.gameObject.GetComponent<Tent>().GoToTent();
                }
                //print("coucou c'est une tente");            
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "TentTrigger")
        {
            AButtonDebug.SetActive(false);
        }
    }
}