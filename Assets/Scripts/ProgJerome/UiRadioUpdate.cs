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
    public TextMeshProUGUI CurrentSoldierNumber;

    static public UiRadioUpdate Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //System.Enum.GetNames(typeof(MilitaryRank));
        
    }

    public void UpdateUI(int i)
    {
        Name.text = DataCenterDay.Instance.CurrentSoldiers[i].Name;
        Age.text = DataCenterDay.Instance.CurrentSoldiers[i].Age;
        Rank.text = DataCenterDay.Instance.CurrentSoldiers[i].Rank.ToString();
        Achievement.text = DataCenterDay.Instance.CurrentSoldiers[i].Achievements;
        CurrentSoldierNumber.text = (i+1).ToString() + '/' + (DataCenterDay.Instance.CurrentSoldiers.Count);
    }
}
