using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DataCenterDay : MonoBehaviour
{
    public static DataCenterDay Instance;

    [HideInInspector] public List<SoldierInfo> CurrentSoldiers = new List<SoldierInfo>();
    [HideInInspector] public List<GameObject> CurrentBullets = new List<GameObject>();
    public List<SoldierCard> CurrentInfoSoldiers = new List<SoldierCard>();
    [HideInInspector] public Tent CurrentTent;
    [HideInInspector] public int BulletsFound = 0;
    [HideInInspector] public bool isABulletFound;

    [SerializeField] private GameObject _map;
    public Camera MainCamera;
    public Camera MapCam;

    // [Space(10)] [Header("Input")] [HideInInspector]
    // public bool isLongPress;
    //
    // private bool isPressed = false;
    // private float pressStartTime = 0f;
    // public float longPressDuration = 3f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (GameData.NumberDays == 2)
            ResetValuesAfterTuto();
    }

    // private void TapOrLongPress()
    // {
    //     if (Gamepad.current.buttonSouth.wasPressedThisFrame)
    //     {
    //         isPressed = true;
    //         pressStartTime = Time.time;
    //     }
    //
    //     if (Gamepad.current.buttonSouth.wasReleasedThisFrame)
    //     {
    //         isPressed = false;
    //         
    //         if (Time.time - pressStartTime > longPressDuration)
    //         {
    //             Debug.Log("Long press detected!");
    //             isLongPress = true;
    //         }
    //         else
    //         {
    //             Debug.Log("Tap detected!");
    //             isLongPress = false;
    //         }
    //     }
    // }

    private void ResetValuesAfterTuto()
    {
        GlobalManager.Instance.GaugesValues[0].ActualValue = 0;
        GlobalManager.Instance.GaugesValues[1].ActualValue = 0;
    }

    public void Clean()
    {
        CurrentSoldiers.Clear();

        for (int i = 0; i < DataCenterDay.Instance.CurrentSoldiers.Count; i++)
            CurrentInfoSoldiers[i].gameObject.SetActive(false);

        CurrentTent = null;
    }

    private void Update()
    {
        //TapOrLongPress();

        if (Gamepad.current.startButton.wasPressedThisFrame)
        {
            _map.SetActive(!_map.activeSelf);
            MainCamera.gameObject.SetActive(!MainCamera.gameObject.activeSelf);
            MapCam.gameObject.SetActive(!MapCam.gameObject.activeSelf);
        }
    }
}