using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCenterDay : MonoBehaviour
{
    public static DataCenterDay Instance;

    [HideInInspector] public List<SoldierInfo> CurrentSoldiers = new List<SoldierInfo>();
    [HideInInspector] public Tent CurrentTent;

    private void Awake()
    {
        Instance = this;
    }


    public void Clean()
    {
        CurrentSoldiers.Clear();
        CurrentTent = null;
    }


}
