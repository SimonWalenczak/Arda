using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class NightManager : MonoBehaviour
{
    #region Init Variables
    [Header("\n------ Previous Upgrade -------\n")]
    [SerializeField] private bool _canUpgrade;
    [SerializeField] private GameObject _generalAnnounce;
    [SerializeField] private TextMeshProUGUI _generalTextVisual;
    [SerializeField] private List<string> _generalText;
    private int index = 0;
    private int _totalSoldierLost;

    [Header("\n----------- Upgrade -----------\n")]
    [SerializeField] private GameObject _upgradePanel;
    public int wheelsType = 1;
    public int bodyCarType = 1;
    public int directionSystemType = 1;

    [Header("\n------------ Zones ------------\n")]
    [SerializeField] private List<Zone> _zones;
    
    [Header("\nChance of rain\n")]
    [SerializeField] private int _rainChance = 15;
    
    [Header("\nChance of hard or soft fight (by range)\n")]
    [SerializeField] private int hardFightChance = 20;
    [SerializeField] private int softFightChance = 10;

    [Header("\nChance of special event\n")]
    [SerializeField] private int _eventChance = 5;
    
    [Header("\nChance of special event (by range)\n")]
    [SerializeField] private int _bombingChance = 66;
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

    public void GeneralAnnounce()
    {
        _generalAnnounce.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                index++;
                if (index == 1)
                {
                    _generalTextVisual.SetText("Général Cruchot : " + _totalSoldierLost + _generalText[index]);
                }
                else if (index == 3)
                {
                    if (GameData.IsSunning)
                    {
                        _generalTextVisual.SetText(_generalText[index] +
                                                   " bon. Vous allez pouvoir rouler sans risque de glissade.");
                    }
                    else
                    {
                        _generalTextVisual.SetText(_generalText[index] +
                                                   " mauvais... Vous allez devoir vous équiper correctement pour éviter les glissades.");
                    }
                }
                else if (index == 4)
                {
                    string bombText =
                        "\n- Qu'il y aura, ne sachant où et quand, un ou des bombardements aujourd'hui alors prenez garde !";
                    string underminedText = "\n- Que l'ennemi a miné une ou plusieurs de nos zones !";
                    string infantryChargeText =
                        "\n- Qu'un assaut ennemi sera effectué dans certaines zones de la carte !";

                    if (GameData.BombingNb + GameData.UnderminedInfiltrationNb + GameData.InfantryChargeNb == 0)
                    {
                        _generalText[index] +=
                            "\n- Qu'il n'y aura pas d'actions de l'ennemi aujourd'hui. Profitez-en, ce n'est que pour la journée !";
                    }

                    if (GameData.BombingNb > 0)
                    {
                        _generalText[index] += bombText;
                    }

                    if (GameData.UnderminedInfiltrationNb > 0)
                    {
                        _generalText[index] += underminedText;
                    }

                    if (GameData.InfantryChargeNb > 0)
                    {
                        _generalText[index] += infantryChargeText;
                    }

                    _generalTextVisual.SetText(_generalText[index]);
                }
                else if (index == 5)
                {
                    string softFightText = "une journée courte de combat ! Vous serez plus vite rentré chez vous !";
                    string hardFightText = "une longue journée de combat ! Attention, ça risque d'être long !";
                    string normalFightText = "une durée moyenne des combats, comme toujours...";

                    if (GameData.SoftFight)
                        _generalText[index] += softFightText;
                    else if (GameData.HardFight)
                        _generalText[index] += hardFightText;
                    else
                        _generalText[index] += normalFightText;

                    _generalTextVisual.SetText(_generalText[index]);
                }
                else if (index >= 7)
                {
                    _canUpgrade = true;
                    _upgradePanel.SetActive(true);
                    _generalAnnounce.SetActive(false);
                }
                else
                {
                    _generalTextVisual.SetText(_generalText[index]);
                }
            }
        
    }
    
    private void Start()
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

        //Total soldiers alived
        foreach (var zone in _zones)
        {
            GameData.TotalSoldier -= zone.SoldierLost;
            _totalSoldierLost += zone.SoldierLost;
        }
        
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
        
        //Visual
        _generalTextVisual.SetText(_generalText[index]);
    }

    private void Update()
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
    }
}