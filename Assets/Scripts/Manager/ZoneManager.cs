using System;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{
    public int IndexCamp;

    [Space (10)]
    public int TotalFirstClassAmputated;
    public int TotalFirstClassDead;
    public int TotalFirstClassSaved;
    
    [Space (10)]
    public int TotalElitAmputated;
    public int TotalElitDead;
    public int TotalElitSaved;

    [Space (10)]
    public int TotalOfficierAmputated;
    public int TotalOfficierDead;
    public int TotalOfficierSaved;

    [Space (10)]
    public int TotalAmputated;
    public int TotalDead;
    public int TotalSaved;

    private void Awake()
    {
        //ResetGameDataValues();
    }


    private void Update()
    {
        //SetValuesToGameData();

        TotalAmputated = TotalFirstClassAmputated + TotalElitAmputated + TotalOfficierAmputated;
        TotalDead = TotalFirstClassDead + TotalElitDead + TotalOfficierDead;
        TotalSaved = TotalFirstClassSaved + TotalElitSaved + TotalOfficierSaved;
    }

    // public void SetValuesToGameData()
    // {
    //     switch (IndexCamp)
    //     {
    //         case 1:
    //             GameData.Zone1FirstClassAmputated = TotalFirstClassAmputated;
    //             GameData.Zone1FirstClassDead = TotalFirstClassDead;
    //             GameData.Zone1FirstClassSaved = TotalFirstClassSaved;
    //
    //             GameData.Zone1ElitClassAmputated = TotalElitAmputated;
    //             GameData.Zone1ElitClassDead = TotalElitDead;
    //             GameData.Zone1ElitClassSaved = TotalElitSaved;
    //             
    //             GameData.Zone1OfficierClassAmputated = TotalOfficierAmputated;
    //             GameData.Zone1OfficierClassDead = TotalOfficierDead;
    //             GameData.Zone1OfficierClassSaved = TotalOfficierSaved;
    //             
    //             GameData.Zone1SoldierAmputated = TotalFirstClassAmputated + TotalElitAmputated + TotalOfficierAmputated;
    //             GameData.Zone1SoldierDead = TotalFirstClassDead + TotalElitDead + TotalOfficierDead;
    //             GameData.Zone1SoldierSaved = TotalFirstClassSaved + TotalElitSaved + TotalOfficierSaved;
    //             break;
    //
    //         case 2:
    //             GameData.Zone2FirstClassAmputated = TotalFirstClassAmputated;
    //             GameData.Zone2FirstClassDead = TotalFirstClassDead;
    //             GameData.Zone2FirstClassSaved = TotalFirstClassSaved;
    //
    //             GameData.Zone2ElitClassAmputated = TotalElitAmputated;
    //             GameData.Zone2ElitClassDead = TotalElitDead;
    //             GameData.Zone2ElitClassSaved = TotalElitSaved;
    //
    //             GameData.Zone2OfficierClassAmputated = TotalOfficierAmputated;
    //             GameData.Zone2OfficierClassDead = TotalOfficierDead;
    //             GameData.Zone2OfficierClassSaved = TotalOfficierSaved;
    //
    //             GameData.Zone2SoldierAmputated = TotalFirstClassAmputated + TotalElitAmputated + TotalOfficierAmputated;
    //             GameData.Zone2SoldierDead = TotalFirstClassDead + TotalElitDead + TotalOfficierDead;
    //             GameData.Zone2SoldierSaved = TotalFirstClassSaved + TotalElitSaved + TotalOfficierSaved;
    //             break;
    //
    //         case 3:
    //             GameData.Zone3FirstClassAmputated = TotalFirstClassAmputated;
    //             GameData.Zone3FirstClassDead = TotalFirstClassDead;
    //             GameData.Zone3FirstClassSaved = TotalFirstClassSaved;
    //
    //             GameData.Zone3ElitClassAmputated = TotalElitAmputated;
    //             GameData.Zone3ElitClassDead = TotalElitDead;
    //             GameData.Zone3ElitClassSaved = TotalElitSaved;
    //
    //             GameData.Zone3OfficierClassAmputated = TotalOfficierAmputated;
    //             GameData.Zone3OfficierClassDead = TotalOfficierDead;
    //             GameData.Zone3OfficierClassSaved = TotalOfficierSaved;
    //
    //             GameData.Zone3SoldierAmputated = TotalFirstClassAmputated + TotalElitAmputated + TotalOfficierAmputated;
    //             GameData.Zone3SoldierDead = TotalFirstClassDead + TotalElitDead + TotalOfficierDead;
    //             GameData.Zone3SoldierSaved = TotalFirstClassSaved + TotalElitSaved + TotalOfficierSaved;
    //             break;
    //
    //         case 4:
    //             GameData.Zone4FirstClassAmputated = TotalFirstClassAmputated;
    //             GameData.Zone4FirstClassDead = TotalFirstClassDead;
    //             GameData.Zone4FirstClassSaved = TotalFirstClassSaved;
    //
    //             GameData.Zone4ElitClassAmputated = TotalElitAmputated;
    //             GameData.Zone4ElitClassDead = TotalElitDead;
    //             GameData.Zone4ElitClassSaved = TotalElitSaved;
    //
    //             GameData.Zone4OfficierClassAmputated = TotalOfficierAmputated;
    //             GameData.Zone4OfficierClassDead = TotalOfficierDead;
    //             GameData.Zone4OfficierClassSaved = TotalOfficierSaved;
    //
    //             GameData.Zone4SoldierAmputated = TotalFirstClassAmputated + TotalElitAmputated + TotalOfficierAmputated;
    //             GameData.Zone4SoldierDead = TotalFirstClassDead + TotalElitDead + TotalOfficierDead;
    //             GameData.Zone4SoldierSaved = TotalFirstClassSaved + TotalElitSaved + TotalOfficierSaved;
    //             break;
    //
    //         case 5:
    //             GameData.Zone5FirstClassAmputated = TotalFirstClassAmputated;
    //             GameData.Zone5FirstClassDead = TotalFirstClassDead;
    //             GameData.Zone5FirstClassSaved = TotalFirstClassSaved;
    //
    //             GameData.Zone5ElitClassAmputated = TotalElitAmputated;
    //             GameData.Zone5ElitClassDead = TotalElitDead;
    //             GameData.Zone5ElitClassSaved = TotalElitSaved;
    //
    //             GameData.Zone5OfficierClassAmputated = TotalOfficierAmputated;
    //             GameData.Zone5OfficierClassDead = TotalOfficierDead;
    //             GameData.Zone5OfficierClassSaved = TotalOfficierSaved;
    //
    //             GameData.Zone5SoldierAmputated = TotalFirstClassAmputated + TotalElitAmputated + TotalOfficierAmputated;
    //             GameData.Zone5SoldierDead = TotalFirstClassDead + TotalElitDead + TotalOfficierDead;
    //             GameData.Zone5SoldierSaved = TotalFirstClassSaved + TotalElitSaved + TotalOfficierSaved;
    //             break;
    //     }
    // }

    // public void ResetGameDataValues()
    // {
    //     //zone 1
    //     GameData.Zone1FirstClassAmputated = 0;
    //     GameData.Zone1FirstClassDead = 0;
    //     GameData.Zone1FirstClassSaved = 0;
    //
    //     GameData.Zone1ElitClassAmputated = 0;
    //     GameData.Zone1ElitClassDead = 0;
    //     GameData.Zone1ElitClassSaved = 0;
    //
    //     GameData.Zone1OfficierClassAmputated = 0;
    //     GameData.Zone1OfficierClassDead = 0;
    //     GameData.Zone1OfficierClassSaved = 0;
    //
    //     GameData.Zone1SoldierAmputated = 0;
    //     GameData.Zone1SoldierDead = 0;
    //     GameData.Zone1SoldierSaved = 0;
    //
    //     //zone 2
    //     GameData.Zone2FirstClassAmputated = 0;
    //     GameData.Zone2FirstClassDead = 0;
    //     GameData.Zone2FirstClassSaved = 0;
    //
    //     GameData.Zone2ElitClassAmputated = 0;
    //     GameData.Zone2ElitClassDead = 0;
    //     GameData.Zone2ElitClassSaved = 0;
    //
    //     GameData.Zone2OfficierClassAmputated = 0;
    //     GameData.Zone2OfficierClassDead = 0;
    //     GameData.Zone2OfficierClassSaved = 0;
    //
    //     GameData.Zone2SoldierAmputated = 0;
    //     GameData.Zone2SoldierDead = 0;
    //     GameData.Zone2SoldierSaved = 0;
    //
    //     //zone 3
    //     GameData.Zone3FirstClassAmputated = 0;
    //     GameData.Zone1FirstClassDead = 0;
    //     GameData.Zone1FirstClassSaved = 0;
    //
    //     GameData.Zone3ElitClassAmputated = 0;
    //     GameData.Zone3ElitClassDead = 0;
    //     GameData.Zone3ElitClassSaved = 0;
    //
    //     GameData.Zone3OfficierClassAmputated = 0;
    //     GameData.Zone3OfficierClassDead = 0;
    //     GameData.Zone3OfficierClassSaved = 0;
    //
    //     GameData.Zone3SoldierAmputated = 0;
    //     GameData.Zone3SoldierDead = 0;
    //     GameData.Zone3SoldierSaved = 0;
    //
    //     //zone 4
    //     GameData.Zone4FirstClassAmputated = 0;
    //     GameData.Zone4FirstClassDead = 0;
    //     GameData.Zone4FirstClassSaved = 0;
    //
    //     GameData.Zone4ElitClassAmputated = 0;
    //     GameData.Zone4ElitClassDead = 0;
    //     GameData.Zone4ElitClassSaved = 0;
    //
    //     GameData.Zone4OfficierClassAmputated = 0;
    //     GameData.Zone4OfficierClassDead = 0;
    //     GameData.Zone4OfficierClassSaved = 0;
    //
    //     GameData.Zone4SoldierAmputated = 0;
    //     GameData.Zone4SoldierDead = 0;
    //     GameData.Zone4SoldierSaved = 0;
    //
    //     //zone 5
    //     GameData.Zone5FirstClassAmputated = 0;
    //     GameData.Zone5FirstClassDead = 0;
    //     GameData.Zone5FirstClassSaved = 0;
    //
    //     GameData.Zone5ElitClassAmputated = 0;
    //     GameData.Zone5ElitClassDead = 0;
    //     GameData.Zone5ElitClassSaved = 0;
    //
    //     GameData.Zone5OfficierClassAmputated = 0;
    //     GameData.Zone5OfficierClassDead = 0;
    //     GameData.Zone5OfficierClassSaved = 0;
    //
    //     GameData.Zone5SoldierAmputated = 0;
    //     GameData.Zone5SoldierDead = 0;
    //     GameData.Zone5SoldierSaved = 0;
    //}
}