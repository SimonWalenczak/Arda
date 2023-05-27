using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradeRadioGridManager : MonoBehaviour
{
    [Header("Transition")] [SerializeField]
    private GameObject FadeOutDay;

    [Header("Reference")] public List<MainButton> MainButtons;
    public List<ButtonUpgrade> ButtonUpgrades;

    [Space(20)] public RectTransform LeftPanelRectTransform;
    public RectTransform RightPanelRectTransform;

    [Space(10)] [Header("Description")] [SerializeField]
    public RectTransform DescriptionPanel;
    [SerializeField] private TMP_Text TitleText;
    [SerializeField] private TMP_Text DescriptionText;
    
    public MainButton actualButton;
    private MainButton previousButton;

    private void Start()
    {
        ResetGameDataRadioUpgrade();
        DescriptionPanel.position = RightPanelRectTransform.position;
        previousButton = actualButton;
    }

    private void ResetGameDataRadioUpgrade()
    {
        GameData.HaveCasque1 = false;
        GameData.HaveCasque2 = false;

        GameData.HaveTube1 = false;
        GameData.HaveTube2 = false;
    }

    public void ValidRadioUpgrade()
    {
        foreach (var mainButton in MainButtons)
        {
            mainButton.DefineRadioEffect();
        }

        StartCoroutine(GoToDayScene());
    }

    IEnumerator GoToDayScene()
    {
        FadeOutDay.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene("bb_LD1");
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