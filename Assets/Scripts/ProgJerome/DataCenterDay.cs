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

    [SerializeField] private GameObject _map;
    public Camera MainCamera;
    public Camera MapCam;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (GameData.NumberDays == 2)
            ResetValuesAfterTuto();
    }

    private void ResetValuesAfterTuto()
    {
        GlobalManager.Instance.GaugesValues[0].ActualValue = 0;
        GlobalManager.Instance.GaugesValues[1].ActualValue = 0;
    }

    public void Clean()
    {
        CurrentSoldiers.Clear();
        CurrentInfoSoldiers.Clear();
        for (int i = 0; i < DataCenterDay.Instance.CurrentSoldiers.Count; i++)
            CurrentInfoSoldiers[i].gameObject.SetActive(true);
        
        CurrentTent = null;
    }

    private void Update()
    {
        if (Gamepad.current.startButton.wasPressedThisFrame)
        {
            _map.SetActive(!_map.activeSelf);
            MainCamera.gameObject.SetActive(!MainCamera.gameObject.activeSelf);
            MapCam.gameObject.SetActive(!MapCam.gameObject.activeSelf);
        }
    }
}