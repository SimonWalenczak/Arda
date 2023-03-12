using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class NightManager : MonoBehaviour
{
    #region Init Variables

    [Header("\n------ Dialog -------\n")] public GeneralDialog _generalDialog;


    [Header("\n------ Previous Upgrade -------\n")] [SerializeField]
    private bool _canUpgrade;

    [SerializeField] private GameObject _generalAnnounce;


    [Header("\n----------- Upgrade -----------\n")] [SerializeField]
    private GameObject _upgradePanel;

    public int wheelsType = 1;
    public int bodyCarType = 1;
    public int directionSystemType = 1;

    [Header("\n------------ Zones ------------\n")] [SerializeField]
    private List<Zone> _zones;

    [Header("\nChance of rain\n")] [SerializeField]
    private int _rainChance = 15;

    [Header("\nChance of hard or soft fight (by range)\n")] [SerializeField]
    private int hardFightChance = 20;

    [SerializeField] private int softFightChance = 10;

    [Header("\nChance of special event\n")] [SerializeField]
    private int _eventChance = 5;

    [Header("\nChance of special event (by range)\n")] [SerializeField]
    private int _bombingChance = 66;

    [SerializeField] private int _underminedInfiltrationChance = 33;
    [SerializeField] private int _infantryChargeChance = 0;

    #endregion


    public void RainingChance()
    {
        int _rainChanceAppear = Random.Range(0, 101);
        if (_rainChanceAppear < _rainChance)
        {
            GameData.IsRainning = true;
            print("rain");
        }
        else
        {
            GameData.IsSunning = true;
            print("sun");
        }
    }

    public void HardSoftFightChance()
    {
        int hardFightChanceAppear = Random.Range(0, 101);
        if (hardFightChanceAppear <= hardFightChance && hardFightChanceAppear > softFightChance)
        {
            GameData.SoftFight = true;
            print("soft fight");
        }
        else if (hardFightChanceAppear <= softFightChance)
        {
            GameData.HardFight = true;
            print("hard fight");
        }
    }

    public void SpecialEventChance()
    {
        foreach (var zone in _zones)
        {
            int _eventChanceAppear = Random.Range(0, 101);
            if (_eventChanceAppear < _eventChance)
            {
                int specialEventChanceAppear = Random.Range(0, 101);
                if (specialEventChanceAppear >= _bombingChance)
                {
                    zone.events = Zone.Event.Bombing;
                    GameData.BombingNb++;
                }
                else if (specialEventChanceAppear >= _underminedInfiltrationChance)
                {
                    zone.events = Zone.Event.UnderminedInfiltration;
                    GameData.UnderminedInfiltrationNb++;
                }
                else if (specialEventChanceAppear >= _infantryChargeChance)
                {
                    zone.events = Zone.Event.InfantryCharge;
                    GameData.InfantryChargeNb++;
                }

                print($"zone {zone.Index} : {zone.events}");
            }
        }
    }

    IEnumerator WaitingForAppearing()
    {
        yield return new WaitForSeconds(3);
        _generalDialog.CanTalk = true;
    }

    private void GeneralSpitch()
    {
    }

    public void GeneralAnnounce()
    {
        _generalAnnounce.SetActive(true);

        StartCoroutine(WaitingForAppearing());
        if (_generalDialog.CanTalk)
        {
            GeneralSpitch();
        }
    }

    private void StartEvents()
    {
        //Events
        if (GameData.NumberDays >= 3)
        {
            //Chance of rain for next day
            RainingChance();

            //Chance of hard or soft fight for next day
            HardSoftFightChance();

            //Chance of special event for next day per zones
            SpecialEventChance();
        }
    }

    private void ResetGlobalVariables()
    {
        //Reset Global Variables
        Cursor.visible = true;
        GameData.CanPlay = true;
        GameData.IsRainning = false;
        GameData.IsSunning = false;
        GameData.SoftFight = false;
        GameData.HardFight = false;
        GameData.BombingNb = 0;
        GameData.UnderminedInfiltrationNb = 0;
        GameData.InfantryChargeNb = 0;
        GameData.WheelsType = 0;
    }

    private void Start()
    {
        _generalDialog = GetComponent<GeneralDialog>();

        ResetGlobalVariables();
        StartEvents();
    }

    private void Update()
    {
        if (_generalDialog.IsFinish)
        {
            UpgradeCar();
        }

        CalculGameDataTotal();
    }

    void CalculGameDataTotal()
    {
        GameData.TotalSoldierAmputated = GameData.Zone1SoldierAmputated + GameData.Zone2SoldierAmputated +
                                         GameData.Zone3SoldierAmputated + GameData.Zone4SoldierAmputated +
                                         GameData.Zone5SoldierAmputated;

        GameData.TotalSoldierDead = GameData.Zone1SoldierDead + GameData.Zone2SoldierDead + GameData.Zone3SoldierDead +
                                    GameData.Zone4SoldierDead + GameData.Zone5SoldierDead;

        GameData.TotalSoldierSaved = GameData.Zone1SoldierSaved + GameData.Zone2SoldierSaved +
                                     GameData.Zone3SoldierSaved + GameData.Zone4SoldierSaved +
                                     GameData.Zone5SoldierSaved;
    }

    void UpgradeCar()
    {
        if (!_canUpgrade && GameData.CanPlay)
            GeneralAnnounce();

        if (Input.GetKeyDown(KeyCode.A))
            wheelsType--;
        if (Input.GetKeyDown(KeyCode.E))
            wheelsType++;

        if (wheelsType > 2)
            wheelsType = 0;
        if (wheelsType < 0)
            wheelsType = 2;

        if (Input.GetKeyDown(KeyCode.Q))
            bodyCarType--;
        if (Input.GetKeyDown(KeyCode.D))
            bodyCarType++;
        if (bodyCarType > 2)
            bodyCarType = 0;
        if (bodyCarType < 0)
            bodyCarType = 2;

        if (Input.GetKeyDown(KeyCode.W))
            directionSystemType--;
        if (Input.GetKeyDown(KeyCode.C))
            directionSystemType++;
        if (directionSystemType > 2)
            directionSystemType = 0;
        if (directionSystemType < 0)
            directionSystemType = 2;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(1);
        }
    }
}