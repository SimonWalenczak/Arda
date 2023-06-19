using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager Instance;

    public int LimitIncrease;

    public List<Gauges> GaugesValues = new List<Gauges>();


    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }

    public void UpdateSucceededValue(int index)
    {
        Debug.Log(GaugesValues[index].ActualValue);
        Debug.Log(GaugesValues[index].Limit);
        GaugesValues[index].ActualValue++;

        if (GaugesValues[index].ActualValue >= GaugesValues[index].Limit)
        {
            print("salam");
            GaugesValues[index].Limit += LimitIncrease;
            //GaugesValues[index].ActualValue = 0;
        }
    }
    
    public void UpdateMissedValue(int index)
    {
        Debug.Log(GaugesValues[index].ActualValue);
        Debug.Log(GaugesValues[index].Limit);
        GaugesValues[index].MissValue++;
    }
}
