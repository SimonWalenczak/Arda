using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SoldierCard : MonoBehaviour
{
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

        switch (InjuryType)
        {
            case 1:
                InjurySprite.color = Color.green;
                break;
            
            case 2:
                InjurySprite.color = Color.yellow;
                break;
            
            case 3:
                InjurySprite.color = Color.red;
                break;
            
            case 4:
                Destroy(gameObject);
                break;
        }

        if (InjuryType <= 3)
        {
            InjuryTime[InjuryType - 1] -= Time.deltaTime;
            if (InjuryTime[InjuryType - 1] <= 0)
            {
                InjuryType++;
            }
        }
    }
}
