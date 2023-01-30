using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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

    public float InjuryTime;

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

    public void Heal()
    {
        if (InjuryType < 3)
        {
            print($"Soldier {LastNameText.text} {FirstNameText.text} is safe.");
        }
        else
        {
            int _deadChance = Random.Range(0, 101);
            if (_deadChance <= 10)
            {
                print($"Soldier {LastNameText.text} {FirstNameText.text} is dead.");
            }
            else
            {
                print($"Soldier {LastNameText.text} {FirstNameText.text} is safe.");
            }
        }
        isOccuped = false;
    }
}
