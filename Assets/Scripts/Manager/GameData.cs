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
    

    //Zones
    public static bool IsRainning;
    public static bool IsSunning;
    public static bool HasFog;
    public static bool SoftFight;
    public static bool HardFight;

    public static int BombingNb;
    public static int UnderminedInfiltrationNb;
    public static int InfantryChargeNb;
    
    //Night Tutorial
    public static bool HasSavesSoldier;
    
    //Upgrade
    //Car
    public static bool HaveFrein1;
    public static bool HaveFrein2;
    public static bool HaveFrein3;
    public static bool HaveMoteur1;
    public static bool HaveMoteur2;
    public static bool HaveMoteur3;
    public static bool HavePneu1;
    public static bool HavePneu2;
    public static bool HavePneu3;
    public static bool HaveChassis1;
    public static bool HaveChassis2;
    public static bool HaveChassis3;
    
    //Radio
    public static bool HaveCasque1;
    public static bool HaveCasque2;

    public static bool HaveTube1;
    public static bool HaveTube2;
}