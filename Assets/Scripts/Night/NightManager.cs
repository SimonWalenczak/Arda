using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class NightManager : MonoBehaviour
{
    #region Init Variables

    [Header("\n------ Dialog -------\n")] public GeneralDialog _generalDialog;
    [SerializeField] private GameObject _generalAnnounce;

    //Soldier Stats Text
    [SerializeField] private TMP_Text SoldierSucceededCount;
    [SerializeField] private TMP_Text SoldierMissedCount;
    [SerializeField] private TMP_Text SoldierLimitText;

    [SerializeField] private TMP_Text OfficierSucceededCount;
    [SerializeField] private TMP_Text OfficierMissedCount;
    [SerializeField] private TMP_Text OfficierLimitText;

    [SerializeField] private TMP_Text GenieSucceededCount;
    [SerializeField] private TMP_Text GenieMissedCount;
    [SerializeField] private TMP_Text GenieLimitText;

    //Total Stats Text
    [SerializeField] private TMP_Text TotalSucceededCount;
    [SerializeField] private TMP_Text TotalMissedCount;

    [Header("\nChance of rain\n")] [SerializeField]
    private int _rainChance = 15;

    [SerializeField] private int _fogChance = 20;
    
    [Header("Fader")]

    public GameObject FadeIn23;
    public GameObject FadeIn24;
    public GameObject FadeOutUpgrade;
    public GameObject FadeOutCredits;

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

        if (GameData.NumberDays == 1)
            FadeIn24.SetActive(false);
        else
            FadeIn23.SetActive(false);
        

        ResetGlobalVariables();
        RainingChance();
        FogChance();
        HardSoftFightChance();
        DisplaySoldierStats();

        StartCoroutine(WaitingForAppearing());
    }

    private void Update()
    {
        if (_generalDialog.CanUpgrade)
        {
            StartCoroutine(GoToDayScene());
        }
    }

    private void DisplaySoldierStats()
    {
        if (GameData.NumberDays == 2)
        {
            SoldierSucceededCount.SetText(GlobalManager.Instance.GaugesValues[0].ActualValue.ToString());
            SoldierMissedCount.SetText(GlobalManager.Instance.GaugesValues[0].MissValue.ToString());
            SoldierLimitText.SetText(GlobalManager.Instance.GaugesValues[0].Limit.ToString());
            OfficierSucceededCount.SetText(GlobalManager.Instance.GaugesValues[1].ActualValue.ToString());
            OfficierMissedCount.SetText(GlobalManager.Instance.GaugesValues[1].MissValue.ToString());
            OfficierLimitText.SetText(GlobalManager.Instance.GaugesValues[1].Limit.ToString());

            GenieSucceededCount.SetText(GlobalManager.Instance.GaugesValues[2].ActualValue.ToString());
            GenieMissedCount.SetText(GlobalManager.Instance.GaugesValues[2].MissValue.ToString());
            GenieLimitText.SetText(GlobalManager.Instance.GaugesValues[2].Limit.ToString());

            TotalSucceededCount.SetText((GlobalManager.Instance.GaugesValues[0].ActualValue +
                                         GlobalManager.Instance.GaugesValues[1].ActualValue +
                                         GlobalManager.Instance.GaugesValues[2].ActualValue).ToString());
            TotalMissedCount.SetText((GlobalManager.Instance.GaugesValues[0].MissValue +
                                      GlobalManager.Instance.GaugesValues[1].MissValue +
                                      GlobalManager.Instance.GaugesValues[2].MissValue).ToString());
        }
        else
        {
            if (GameData.HasSavesSoldier)
            {
                SoldierSucceededCount.SetText("1");
                OfficierSucceededCount.SetText("0");
            }
            else
            {
                SoldierSucceededCount.SetText("0");
                OfficierSucceededCount.SetText("1");
            }
        }
    }

    IEnumerator WaitingForAppearing()
    {
        yield return new WaitForSeconds(3);
        _generalDialog.CanTalk = true;
    }

    public void FogChance()
    {
        int _fogChanceAppear = Random.Range(0, 101);
        if (_fogChanceAppear < _fogChance)
        {
            GameData.HasFog = true;
            print("fog active");
        }
        else
        {
            GameData.HasFog = false;
            print("fog desactive");
        }
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
        if (hardFightChanceAppear <= 66 && hardFightChanceAppear > 33)
        {
            GameData.SoftFight = true;
            print("soft fight");
        }
        else if (hardFightChanceAppear <= 33)
        {
            GameData.HardFight = true;
            print("hard fight");
        }
        else
        {
            GameData.SoftFight = false;
            GameData.HardFight = false;
            print("normal fight");
        }
    }

    // IEnumerator GoToUpgradeScene()
    // {
    //     FadeOutUpgrade.SetActive(true);
    //     yield return new WaitForSeconds(3.5f);
    //     SceneManager.LoadScene("Upgrade");
    // }
    
    IEnumerator GoToDayScene()
    {
        FadeOutUpgrade.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene("REBOOT_FINAL(finalement)");
    }
}