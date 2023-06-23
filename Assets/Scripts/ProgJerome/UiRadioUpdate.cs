using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class UiRadioUpdate : MonoBehaviour
{
    public TextMeshProUGUI FirstName;
    public TextMeshProUGUI LastName;
    public TextMeshProUGUI Age;
    public TextMeshProUGUI Rank;
    public TextMeshProUGUI Achievement;
    public TextMeshProUGUI CurrentSoldierNumber;

    public static UiRadioUpdate Instance;

    public RectTransform SoldiersPanelOrigin;
    public Vector2 PositionOrigin;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PositionOrigin = SoldiersPanelOrigin.anchoredPosition;
    }

    public void ApplyInfoSoldier(int i)
    {
        FirstName.text = "Nom : " + DataCenterDay.Instance.CurrentSoldiers[i].Name.Split(' ')[0];
        LastName.text = "Prénom : " + DataCenterDay.Instance.CurrentSoldiers[i].Name.Split(' ')[1];
        Age.text = "Age : " + DataCenterDay.Instance.CurrentSoldiers[i].Age;
        Rank.text = "Grade : " + DataCenterDay.Instance.CurrentSoldiers[i].Rank;
        Achievement.text = DataCenterDay.Instance.CurrentSoldiers[i].Achievements;
    }

    public void UpdateUI(int i)
    {
        CurrentSoldierNumber.text = (i + 1).ToString() + '/' + (DataCenterDay.Instance.CurrentSoldiers.Count);
    }
}