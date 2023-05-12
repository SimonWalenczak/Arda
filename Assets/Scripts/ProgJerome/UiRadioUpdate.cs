using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiRadioUpdate : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Age;
    public TextMeshProUGUI Rank;
    public TextMeshProUGUI Achievement;

    static public UiRadioUpdate Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateUI(int i)
    {
        Name.text = DataCenterDay.Instance.CurrentSoldiers[i].Name;
        Age.text = DataCenterDay.Instance.CurrentSoldiers[i].Age;
        Rank.text = DataCenterDay.Instance.CurrentSoldiers[i].MilitaryRank;
        Achievement.text = DataCenterDay.Instance.CurrentSoldiers[i].Achievements;
    }


}