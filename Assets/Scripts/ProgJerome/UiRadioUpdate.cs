using UnityEngine;
using TMPro;

public class UiRadioUpdate : MonoBehaviour
{
    public TextMeshProUGUI FirstName;
    public TextMeshProUGUI LastName;
    public TextMeshProUGUI Age;
    public TextMeshProUGUI Rank;
    public TextMeshProUGUI Achievement;
    public TextMeshProUGUI CurrentSoldierNumber;

    static public UiRadioUpdate Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateUI(int i)
    {
        FirstName.text = "Nom : " + DataCenterDay.Instance.CurrentSoldiers[i].Name.Split(' ')[0];
        LastName.text = "Prénom : " + DataCenterDay.Instance.CurrentSoldiers[i].Name.Split(' ')[1];
        Age.text = "Age : " + DataCenterDay.Instance.CurrentSoldiers[i].Age;
        Rank.text = "Grade : " + DataCenterDay.Instance.CurrentSoldiers[i].Rank;
        Achievement.text = DataCenterDay.Instance.CurrentSoldiers[i].Achievements;
        
        CurrentSoldierNumber.text = (i+1).ToString() + '/' + (DataCenterDay.Instance.CurrentSoldiers.Count);
    }
}
