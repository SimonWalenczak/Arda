using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateSoldier : MonoBehaviour
{
    public List<String> LastName;
    public List<String> FirstName;
    public List<String> Situation;

    public List<String> MilitaryRank;

    [SerializeField] private float _appearingChance;
    
    [Space(10), Header("MilitaryClass")]
    [SerializeField] private float _captainClassChance = 10;
    [SerializeField] private float _majorClassChance = 35;
    
    [Space(10), Header("InjuryState")]
    [SerializeField] private float _hardInjuryChance = 10;
    [SerializeField] private float _mediumInjuryChance = 35;

    private Camp _camp;
    private void Start()
    {
        _camp = GetComponent<Camp>();
    }

    public void GeneratorSoldier()
    {
        int _appearingChanceAppear = Random.Range(0, 101);
        if (_camp.SoldierInPlace < 4 && _appearingChanceAppear <= _appearingChance)
        {
            _camp._soldierCard[_camp.SoldierInPlace].isOccuped = true;
            _camp.SoldierInPlace++;
            string firstName = FirstName[Random.Range(0, FirstName.Count)];
            string lastName = LastName[Random.Range(0, LastName.Count)];
            string situation = Situation[Random.Range(0, Situation.Count)];
            int age = 0;
            
            int _militaryRankChance = Random.Range(0, 101);
            if (_militaryRankChance <= _captainClassChance)
            {
                _camp._soldierCard[_camp.SoldierInPlace - 1].MilitaryRankText.SetText("Rank : " + MilitaryRank[2]);
                age = Random.Range(30, 36);
            }
            else if (_militaryRankChance <= _majorClassChance)
            {
                _camp._soldierCard[_camp.SoldierInPlace - 1].MilitaryRankText.SetText("Rank : " + MilitaryRank[1]);
                age = Random.Range(25, 36);
            }
            else
            {
                _camp._soldierCard[_camp.SoldierInPlace - 1].MilitaryRankText.SetText("Rank : " + MilitaryRank[0]);
                age = Random.Range(18, 36);
            }


            int _injuryChance = Random.Range(0, 101);
            if (_injuryChance <= _hardInjuryChance)
            {
                _camp._soldierCard[_camp.SoldierInPlace - 1].InjuryType = 3;
            }
            else if (_injuryChance <= _mediumInjuryChance)
            {
                _camp._soldierCard[_camp.SoldierInPlace - 1].InjuryType = 2;
            }
            else
            {
                _camp._soldierCard[_camp.SoldierInPlace - 1].InjuryType = 1;
            }
            
            _camp._soldierCard[_camp.SoldierInPlace - 1].FirstNameText.SetText(firstName);
            _camp._soldierCard[_camp.SoldierInPlace - 1].LastNameText.SetText(lastName);
            _camp._soldierCard[_camp.SoldierInPlace - 1].AgeText.SetText(age.ToString());
            _camp._soldierCard[_camp.SoldierInPlace - 1].SituationText.SetText(situation);
            
            Debug.Log("Génération de soldat\nPrénom : " + firstName + "\nNom : " + lastName + "\nAge : " + age + "\nSituation : " + situation);
        }
    }
}