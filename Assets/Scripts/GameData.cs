using UnityEngine;

public class GameData
{
    //Game
    public static bool CanPlay;
    public static int NumberDays = 3;
    //Player
    public static Transform PlayerPos;

    public static int WheelsType;

    //Soldier
    public static int TotalSoldier = 400000;
    public static int SoldierZone1 = 80000;
    public static int SoldierZone2 = 80000;
    public static int SoldierZone3 = 80000;
    public static int SoldierZone4 = 80000;
    public static int SoldierZone5 = 80000;
    
    //Zones
    public static bool IsRainning;
    public static bool IsSunning;
    public static bool SoftFight;
    public static bool HardFight;

    public static int BombingNb;
    public static int UnderminedInfiltrationNb;
    public static int InfantryChargeNb;
}