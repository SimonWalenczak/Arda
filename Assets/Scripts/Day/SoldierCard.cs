using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SoldierCard : MonoBehaviour
{
    public bool isOccuped;
    public bool isSelected;
    public int index;

    public TextMeshProUGUI LastNameText;
    public TextMeshProUGUI FirstNameText;
    public TextMeshProUGUI AgeText;
    public TextMeshProUGUI SituationText;
    public TextMeshProUGUI MilitaryRankText;
    public Image InjurySprite;
    public int InjuryType;

    public List<float> InjuryTime;
    private float _resetInjuryTime;
    

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
