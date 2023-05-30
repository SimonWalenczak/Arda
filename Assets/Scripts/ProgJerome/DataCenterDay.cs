using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCenterDay : MonoBehaviour
{
    public static DataCenterDay Instance;

    [HideInInspector] public List<SoldierInfo> CurrentSoldiers = new List<SoldierInfo>();
    [HideInInspector] public List<GameObject> CurrentBullets = new List<GameObject>();
    [HideInInspector] public Tent CurrentTent;
    [HideInInspector] public int BulletsFound = 0;

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
        CurrentTent = null;
    }
}