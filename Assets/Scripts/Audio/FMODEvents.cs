using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [Header("Collision")]
    public EventReference CollisionEvent;

    public static FMODEvents Instance;


    private void Awake()
    {
        Instance = this;
    }
}
