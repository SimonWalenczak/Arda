using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Soldier : MonoBehaviour
{
    public bool isOccuped;
    public bool isSelected;
    public int index;

    public string LastName;
    public string FirstName;
    public string Age;
    public string Situation;
    public string MilitaryRank;
    public int InjuryType;

    public float InjuryTime;
    
    
    public void Heal()
    {
        if (InjuryType < 3)
        {
            print($"Soldier {LastName} {FirstName} is safe.");
        }
        else
        {
            int _deadChance = Random.Range(0, 101);
            if (_deadChance <= 10)
            {
                print($"Soldier {LastName} {FirstName} is dead.");
            }
            else
            {
                print($"Soldier {LastName} {FirstName} is safe.");
            }
        }
        isOccuped = false;
    }
}