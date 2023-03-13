using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class NightManager : MonoBehaviour
{
    #region Init Variables

    [Header("\n------ Dialog -------\n")] public GeneralDialog _generalDialog;
    [SerializeField] private GameObject _generalAnnounce;

    [SerializeField] private TMP_Text FirstClassAmputatedText;
    [SerializeField] private TMP_Text FirstClassDeadText;
    [SerializeField] private TMP_Text EliteAmputatedText;
    [SerializeField] private TMP_Text EliteDeadText;
    [SerializeField] private TMP_Text OfficerAmputatedText;
    [SerializeField] private TMP_Text OfficierDeadText;

    [SerializeField] private TMP_Text FirstClassSavedText;
    [SerializeField] private TMP_Text EliteSavedText;
    [SerializeField] private TMP_Text OfficierSavedText;
    
    [Header("\nChance of rain\n")] 
    [SerializeField] private int _rainChance = 15;

    [Header("\nChance of hard or soft fight (by range)\n")] 
    [SerializeField] private int hardFightChance = 20;
    [SerializeField] private int softFightChance = 10;

    
    [Header("\n----------- Upgrade -----------\n")] [SerializeField]
    private GameObject _upgradePanel;

    [SerializeField] private List<Item> Items;
    [SerializeField] private int index;
    //[Header("\n------------ Zones ------------\n")] 
    //[SerializeField] private List<Zone> _zones;



    #endregion

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
        index = 1;
        ResetGlobalVariables();
        CalculGameDataTotal();
        SetValueForBilan();
        StartEvents();
        StartCoroutine(WaitingForAppearing());
    }

    private void Update()
    {
        if (_generalDialog.CanUpgrade)
            UpgradeCar();
    }

    private void StartEvents()
    {
        if (GameData.NumberDays >= 0)
        {
            RainingChance();
            HardSoftFightChance();
        }
    }

    private void CalculGameDataTotal()
    {
        //First Class
        GameData.TotalFirstClassAmputated = GameData.Zone1FirstClassAmputated + GameData.Zone2FirstClassAmputated +
                                            GameData.Zone3FirstClassAmputated + GameData.Zone4FirstClassAmputated +
                                            GameData.Zone5FirstClassAmputated;

        GameData.TotalFirstClassDead = GameData.Zone1FirstClassDead + GameData.Zone2FirstClassDead +
                                       GameData.Zone3FirstClassDead + GameData.Zone4FirstClassDead +
                                       GameData.Zone5FirstClassDead;

        GameData.TotalFirstClassSaved = GameData.Zone1FirstClassSaved + GameData.Zone2FirstClassSaved +
                                        GameData.Zone3FirstClassSaved + GameData.Zone4FirstClassSaved +
                                        GameData.Zone5FirstClassSaved;

        //Elit Class
        GameData.TotalElitAmputated = GameData.Zone1ElitClassAmputated + GameData.Zone2ElitClassAmputated +
                                      GameData.Zone3ElitClassAmputated + GameData.Zone4ElitClassAmputated +
                                      GameData.Zone5ElitClassAmputated;

        GameData.TotalElitDead = GameData.Zone1ElitClassDead + GameData.Zone2ElitClassDead +
                                 GameData.Zone3ElitClassDead + GameData.Zone4ElitClassDead +
                                 GameData.Zone5ElitClassDead;

        GameData.TotalElitClassSaved = GameData.Zone1ElitClassSaved + GameData.Zone2ElitClassSaved +
                                       GameData.Zone3ElitClassSaved + GameData.Zone4ElitClassSaved +
                                       GameData.Zone5ElitClassSaved;

        //Officier Class
        GameData.TotalOfficierAmputated = GameData.Zone1OfficierClassAmputated + GameData.Zone2OfficierClassAmputated +
                                          GameData.Zone3OfficierClassAmputated + GameData.Zone4OfficierClassAmputated +
                                          GameData.Zone5OfficierClassAmputated;

        GameData.TotalOfficierDead = GameData.Zone1OfficierClassDead + GameData.Zone2OfficierClassDead +
                                     GameData.Zone3OfficierClassDead + GameData.Zone4OfficierClassDead +
                                     GameData.Zone5OfficierClassDead;

        GameData.TotalOfficierSaved = GameData.Zone1OfficierClassSaved + GameData.Zone2OfficierClassSaved +
                                      GameData.Zone3OfficierClassSaved + GameData.Zone4OfficierClassSaved +
                                      GameData.Zone5OfficierClassSaved;

        //Total
        GameData.TotalSoldierAmputated = GameData.TotalFirstClassAmputated + GameData.TotalElitAmputated +
                                         GameData.TotalOfficierAmputated;

        GameData.TotalSoldierDead = GameData.TotalFirstClassAmputated + GameData.TotalElitAmputated +
                                    GameData.TotalOfficierAmputated;

        GameData.TotalSoldierSaved = GameData.TotalFirstClassSaved + GameData.TotalElitClassSaved +
                                     GameData.TotalOfficierSaved;
    }

    private void SetValueForBilan()
    {
        FirstClassAmputatedText.SetText(GameData.TotalFirstClassAmputated.ToString());
        FirstClassDeadText.SetText(GameData.TotalFirstClassDead.ToString());

        EliteAmputatedText.SetText(GameData.TotalElitAmputated.ToString());
        EliteDeadText.SetText(GameData.TotalElitDead.ToString());

        OfficerAmputatedText.SetText(GameData.TotalOfficierAmputated.ToString());
        OfficierDeadText.SetText(GameData.TotalOfficierDead.ToString());

        FirstClassSavedText.SetText(GameData.TotalFirstClassSaved.ToString());
        EliteSavedText.SetText(GameData.TotalElitClassSaved.ToString());
        OfficierSavedText.SetText(GameData.TotalOfficierSaved.ToString());
    }

    IEnumerator WaitingForAppearing()
    {
        yield return new WaitForSeconds(3);
        _generalDialog.CanTalk = true;
    }

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

    void UpgradeCar()
    {
        _generalAnnounce.SetActive(false);
        _upgradePanel.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Q))
            index--;
        if (Input.GetKeyDown(KeyCode.D))
            index++;
        
        if (index > Items.Count)
            index = 1;
        if (index < 1)
            index = Items.Count;
        
        foreach (var item in Items)
        {
            if (item.index == index)
                item.isSelected = true;
            else
                item.isSelected = false;
            
            if (item.isSelected)
            {
                print(item.index);
                item.transform.DOScale(new Vector3(1.7f, 1.7f, 1.7f), 1);
            }
            else
            {
                item.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 1);
            }
        }
            
            
        // if (Input.GetKeyDown(KeyCode.Return))
        // {
        //     SceneManager.LoadScene(1);
        // }
    }
}