using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    GlobalManager Instance;

    public float ActualSoldierCount;
    public float ActualOfficerCount;
    public float ActualEngineerCount;




    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
