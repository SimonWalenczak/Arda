using UnityEngine;

public class GameData
{
    //Game
    public static bool CanPlay;
    public static bool Started = false;
    public static int NumberDays = 1;


    //Player
    public static float speed = 20;
    public static float turnSpeed = 60;

    public static int healTimeReduc = 0;

    
    //Soldier
    public static float _maxGaugeSoldier;
    public static float _maxGaugeOfficier;
    public static float _maxGaugeMedecin;

    public static float _actualGaugeSoldier;
    public static float _actualGaugeOfficier;
    public static float _actualGaugeMedecin;
    
    //Zones
    public static bool IsRainning;
    public static bool IsSunning;
    public static bool SoftFight;
    public static bool HardFight;

    public static int BombingNb;
    public static int UnderminedInfiltrationNb;
    public static int InfantryChargeNb;
    
    //Night Tutorial

    public static bool HasSavesSoldier;
}