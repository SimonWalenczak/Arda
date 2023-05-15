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
    public float _maxGaugeSoldier;
    public float _maxGaugeOfficier;
    public float _maxGaugeMedecin;

    public float _actualGaugeSoldier;
    public float _actualGaugeOfficier;
    public float _actualGaugeMedecin;
    
    //Zones
    public static bool IsRainning;
    public static bool IsSunning;
    public static bool SoftFight;
    public static bool HardFight;

    public static int BombingNb;
    public static int UnderminedInfiltrationNb;
    public static int InfantryChargeNb;
}