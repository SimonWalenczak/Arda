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

    public int indexSoldier;

    public List<RectTransform> SoldiersPanelOrigin;
    public RectTransform lastSelected;
    public RectTransform actualSelected;
    
    private void Awake()
    {
        Instance = this;
        indexSoldier = 0;
        lastSelected = SoldiersPanelOrigin[0];
        actualSelected = SoldiersPanelOrigin[0];
        actualSelected.localPosition = Vector3.Lerp(actualSelected.localPosition, new Vector3(-280, actualSelected.localPosition.y, actualSelected.localPosition.z), 1);
    }
    
    private void Update()
    {
        if (Gamepad.current.dpad.down.wasPressedThisFrame /*|| Gamepad.current.dpad.right.wasPressedThisFrame*/)
        {
            indexSoldier++;
            if (indexSoldier > DataCenterDay.Instance.CurrentSoldiers.Count - 1)
                indexSoldier = 0;
            
            lastSelected = actualSelected;
            actualSelected = SoldiersPanelOrigin[indexSoldier];
            
            lastSelected.localPosition = Vector3.Lerp(lastSelected.localPosition, new Vector3(-365, lastSelected.localPosition.y, lastSelected.localPosition.z), 1);
            actualSelected.localPosition = Vector3.Lerp(actualSelected.localPosition, new Vector3(-280, actualSelected.localPosition.y, actualSelected.localPosition.z), 1);
        }

        if (Gamepad.current.dpad.up.wasPressedThisFrame /*||Gamepad.current.dpad.left.wasPressedThisFrame*/)
        {
            indexSoldier--;
            if (indexSoldier < 0)
                indexSoldier = DataCenterDay.Instance.CurrentSoldiers.Count - 1;

            lastSelected = actualSelected;
            actualSelected = SoldiersPanelOrigin[indexSoldier];
            
            lastSelected.localPosition = Vector3.Lerp(lastSelected.localPosition, new Vector3(-365, lastSelected.localPosition.y, lastSelected.localPosition.z), 1);
            actualSelected.localPosition = Vector3.Lerp(actualSelected.localPosition, new Vector3(-280, actualSelected.localPosition.y, actualSelected.localPosition.z), 1);
        }
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