using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum Effect
{
    //Car
    Frein1,
    Frein2,
    Frein3,
    Pneu1,
    Pneu2,
    Pneu3,
    Moteur1,
    Moteur2,
    Moteur3,
    Chassis1,
    Chassis2,
    Chassis3,

    //Radio
    Casque1,
    Casque2,
    Tube1,
    Tube2
}

public class UpgradeCarGridManager : MonoBehaviour
{
    [Header("Types Upgrades")]
    [SerializeField] private GameObject CarUpgradePanel;
    [SerializeField] private GameObject RadioUpgradePanel;
    
    [Space(10)][Header("Reference")] public List<MainButton> MainButtons;
    public List<ButtonUpgrade> ButtonUpgrades;
    public Button PrimaryCarButton;
    public Button PrimaryRadioButton;

    [Space(20)] public RectTransform LeftPanelRectTransform;
    public RectTransform RightPanelRectTransform;
    public RectTransform DescriptionPanel;
    [SerializeField] private TMP_Text DescriptionText;

    [Space(10)] [Header("Description")] [SerializeField]
    private TMP_Text TitleText;

    private MainButton actualButton;
    private MainButton previousButton;

    private void Start()
    {
        PrimaryCarButton.Select();
        ResetGameDataCarUpgrade();
        actualButton = PrimaryCarButton.GetComponent<MainButton>();
        previousButton = PrimaryCarButton.GetComponent<MainButton>();

        GetComponent<UpgradeRadioGridManager>().actualButton = PrimaryRadioButton.GetComponent<MainButton>();

        DescriptionPanel.position = RightPanelRectTransform.position;
    }

    private void ResetGameDataCarUpgrade()
    {
        GameData.HaveFrein1 = false;
        GameData.HaveFrein2 = false;
        GameData.HaveFrein3 = false;
        GameData.HaveMoteur1 = false;
        GameData.HaveMoteur2 = false;
        GameData.HaveMoteur3 = false;
        GameData.HavePneu1 = false;
        GameData.HavePneu2 = false;
        GameData.HavePneu3 = false;
        GameData.HaveChassis1 = false;
        GameData.HaveChassis2 = false;
        GameData.HaveChassis3 = false;
    }

    public void ValidCarUpgrade()
    {
        foreach (var mainButton in MainButtons)
        {
            mainButton.DefineCarEffect();
        }
        
        RadioUpgradePanel.SetActive(true);
        PrimaryRadioButton.Select();
        CarUpgradePanel.SetActive(false);
    }


    private void UpdateMainButtons()
    {
        foreach (var mainButton in MainButtons)
        {
            if (mainButton.IsSelect)
            {
                actualButton = mainButton;

                if (actualButton != previousButton)
                {
                    previousButton.IsSelect = false;
                    previousButton = actualButton;

                    if (actualButton.IsLeftSide)
                        DescriptionPanel.position = RightPanelRectTransform.position;
                    else
                        DescriptionPanel.position = LeftPanelRectTransform.position;
                }
            }

            mainButton.GridButton.SetActive(mainButton.IsSelect);
            TitleText.text = actualButton.Title;
        }
    }

    private void UpdateUpgradeButtons()
    {
        foreach (var buttonUpgrade in ButtonUpgrades)
        {
            if (buttonUpgrade.IsSelect)
            {
                DescriptionText.text = buttonUpgrade.TextDescription;
                break;
            }
            else
            {
                DescriptionText.text = "";
            }
        }
    }

    private void Update()
    {
        UpdateMainButtons();
        UpdateUpgradeButtons();
    }
}