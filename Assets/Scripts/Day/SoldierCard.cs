using System;
using DG.Tweening;
using UnityEngine;
using TMPro;

public class SoldierCard : MonoBehaviour
{
    public bool isSelected;
    public int index;

    public TextMeshProUGUI LastNameText;
    public TextMeshProUGUI FirstNameText;
    public TextMeshProUGUI AgeText;
    public TextMeshProUGUI SituationText;
    

    private void Update()
    {
        if (isSelected)
        {
            transform.DOScale(1.2f,0.2f);
        }
        else
        {
            transform.DOScale(1, 0.2f);
        }
    }
}
