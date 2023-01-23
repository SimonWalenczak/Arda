using System;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public enum Event
    {
        Bombing,
        UnderminedInfiltration,
        InfantryCharge,
    }

    public Enum events;
    
    public int Index;
    public int SoldierAlived;
    public int SoldierLost = 0;
    private int _soldierSaved;

    private void Start()
    {
        switch (Index)
        {
            case 1:
                SoldierAlived = GameData.SoldierZone1;
                break;
            case 2:
                SoldierAlived = GameData.SoldierZone2;
                break;
            case 3:
                SoldierAlived = GameData.SoldierZone3;
                break;
            case 4:
                SoldierAlived = GameData.SoldierZone4;
                break;
            case 5:
                SoldierAlived = GameData.SoldierZone5;
                break;
        }
    }
}
