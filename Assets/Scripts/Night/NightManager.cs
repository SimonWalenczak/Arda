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

    [SerializeField] GameObject UpgradePopup;
    [SerializeField] TMP_Text UpgradePopupText;

    [SerializeField] private int nbUpgradeapplicated;
    public List<GameObject> UpgradeApplicated;
    [SerializeField] private float offsetX;
    [SerializeField] private GameObject FadeOut;


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
        for(int i = 0; i < UpgradeApplicated.Count; i++)
        {
            UpgradeApplicated[i].SetActive(false);
        }
        
        ResetGlobalVariables();
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
        SceneManager.LoadScene("ScenePascal");
    }
    void UpgradeCar()
    {
        _generalAnnounce.SetActive(false);
        _upgradePanel.SetActive(true);

        if (Gamepad.current.leftStick.left.wasPressedThisFrame)
            index--;
        if (Gamepad.current.leftStick.right.wasPressedThisFrame)
            index++;

        if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            StartCoroutine(GoToDayScene());

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
                UpgradePopup.transform.position = new Vector3(item.transform.position.x + 3.5f, item.transform.position.y + 1f,  item.transform.position.z - 1);
                UpgradePopupText.SetText(item.previewEffect);

                if (Gamepad.current.buttonNorth.wasPressedThisFrame && item.isPicked == false)
                {
                    item.isPicked = true;
                    item.Excute();
                    nbUpgradeapplicated++;
                    
                    for (int i = 0; i < nbUpgradeapplicated; i++)
                    {
                        UpgradeApplicated[i].SetActive(true);
                    }

                    UpgradeApplicated[nbUpgradeapplicated - 1].transform.DOMoveX(UpgradeApplicated[nbUpgradeapplicated - 1].transform.position.x + offsetX, 1);
                    UpgradeApplicated[nbUpgradeapplicated-1].GetComponentInChildren<TMP_Text>().SetText(item.effect);
                    
                    print(item.itemName);
                }
            }
            else
            {
                item.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 1);
            }
        }
    }
}