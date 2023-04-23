using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
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

    [Header("\nChance of rain\n")] [SerializeField]
    private int _rainChance = 15;

    [Header("\nChance of hard or soft fight (by range)\n")] [SerializeField]
    private int hardFightChance = 20;

    [SerializeField] private int softFightChance = 10;


    [Header("\n----------- Upgrade -----------\n")] [SerializeField]
    private GameObject _upgradePanel;

    [SerializeField] private List<Item> Items;
    [SerializeField] private int index;

    [SerializeField] GameObject UpgradePopup;
    [SerializeField] TMP_Text UpgradePopupText;

    [SerializeField] private int nbUpgradeapplicated;
    public List<GameObject> UpgradeApplicated;
    [SerializeField] private float offsetX;
    [SerializeField] private GameObject FadeOut;


    [Space(10)] [Header("Input")] private bool canGoToNextDay;
    float pressTime = 0;

    bool isLongPress = false;
    //[Header("\n------------ Zones ------------\n")] 
    //[SerializeField] private List<Zone> _zones;

    #endregion

    private void ResetGlobalVariables()
    {
        //Reset Global Variables
        GameData.CanPlay = true;
        GameData.IsRainning = false;
        GameData.IsSunning = false;
        GameData.SoftFight = false;
        GameData.HardFight = false;
        GameData.BombingNb = 0;
        GameData.UnderminedInfiltrationNb = 0;
        GameData.InfantryChargeNb = 0;
    }

    private void Start()
    {
        Cursor.visible = false;
        _generalDialog = GetComponent<GeneralDialog>();
        index = 1;
        for (int i = 0; i < UpgradeApplicated.Count; i++)
        {
            UpgradeApplicated[i].SetActive(false);
        }

        ResetGlobalVariables();
        CalculGameDataTotal();
        SetValueForBilan();
        StartEvents();
        StartCoroutine(WaitingForAppearing());
    }

    private void Update()
    {
        TapOrLongTouch();

        if (_generalDialog.CanUpgrade)
            UpgradeCar();

        if (Gamepad.current.bButton.wasReleasedThisFrame && canGoToNextDay == true)
        {
            StartCoroutine(GoToDayScene());
        }
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

    IEnumerator GoToDayScene()
    {
        FadeOut.SetActive(true);
        GameData.NumberDays++;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }

    void UpgradeCar()
    {
        canGoToNextDay = true;
        _generalAnnounce.SetActive(false);
        _upgradePanel.SetActive(true);

        if (Gamepad.current.dpad.left.wasPressedThisFrame)
        {
            print(("moins"));
            index--;
        }

        if (Gamepad.current.dpad.right.wasPressedThisFrame)
        {
            print(("plus"));
            index++;
        }

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
                UpgradePopup.transform.position = new Vector3(item.transform.position.x + 3.5f,
                    item.transform.position.y + 1f, item.transform.position.z - 1);
                UpgradePopupText.SetText(item.previewEffect);

                if (Gamepad.current.aButton.wasPressedThisFrame
                    && item.isPicked == false)
                {
                    item.isPicked = true;
                    item.Excute();
                    nbUpgradeapplicated++;

                    for (int i = 0; i < nbUpgradeapplicated; i++)
                    {
                        UpgradeApplicated[i].SetActive(true);
                    }

                    UpgradeApplicated[nbUpgradeapplicated - 1].transform
                        .DOMoveX(UpgradeApplicated[nbUpgradeapplicated - 1].transform.position.x + offsetX, 1);
                    UpgradeApplicated[nbUpgradeapplicated - 1].GetComponentInChildren<TMP_Text>().SetText(item.effect);

                    print(item.itemName);
                }
            }
            else
            {
                item.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 1);
            }
        }
    }

    private void TapOrLongTouch()
    {
        if (Gamepad.current.aButton.isPressed || Gamepad.current.bButton.isPressed)
        {
            pressTime += Time.deltaTime;

            if (pressTime >= 0.5f)
                isLongPress = true;
            else
                isLongPress = false;
        }
        else if (Gamepad.current.aButton.wasReleasedThisFrame || Gamepad.current.bButton.wasReleasedThisFrame)
        {
            pressTime = 0;
        }
    }
}