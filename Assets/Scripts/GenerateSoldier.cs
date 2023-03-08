using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class GenerateSoldier : MonoBehaviour
{
    public List<String> LastName;
    public List<String> FirstName;
    public List<String> Situation;

    public List<String> MilitaryRank;

    [SerializeField] private float _appearingChance;

    [Space(10), Header("MilitaryClass")] [SerializeField]
    private float _captainClassChance = 10;

    [SerializeField] private float _majorClassChance = 35;

    [Space(10), Header("InjuryState")] [SerializeField]
    private float _hardInjuryChance = 10;

    [SerializeField] private float _mediumInjuryChance = 35;
    public List<float> InjuryTimer;

    private Camp _camp;

    public TextMeshProUGUI SoldierDebugText;

    private void Start()
    {
        _camp = GetComponent<Camp>();
        SoldierDebugText.text = "";
    }

    // public void GeneratorSoldier()
    // {
    //     int _appearingChanceAppear = Random.Range(0, 101);
    //     
    //     if (_camp.SoldierInPlace < _camp.SoldierInPlaceMax && _appearingChanceAppear <= _appearingChance) 
    //     {
    //         _camp._playerController.soldiers.Add(_camp._soldiers[_camp.SoldierInPlace]);
    //         _camp._soldiers[_camp.SoldierInPlace].isOccuped = true;
    //         _camp.SoldierInPlace++;
    //         string firstName = FirstName[Random.Range(0, FirstName.Count)];
    //         string lastName = LastName[Random.Range(0, LastName.Count)];
    //         string situation = Situation[Random.Range(0, Situation.Count)];
    //         int age = 0;
    //         
    //         int _militaryRankChance = Random.Range(0, 101);
    //         if (_militaryRankChance <= _captainClassChance)
    //         {
    //             _camp._soldiers[_camp.SoldierInPlace - 1].MilitaryRank = "Rank : " + MilitaryRank[2];
    //             age = Random.Range(30, 36);
    //         }
    //         else if (_militaryRankChance <= _majorClassChance)
    //         {
    //             _camp._soldiers[_camp.SoldierInPlace - 1].MilitaryRank = "Rank : " + MilitaryRank[1];
    //             age = Random.Range(25, 36);
    //         }
    //         else
    //         {
    //             _camp._soldiers[_camp.SoldierInPlace - 1].MilitaryRank = "Rank : " + MilitaryRank[0];
    //             age = Random.Range(18, 36);
    //         }
    //
    //
    //         int _injuryChance = Random.Range(0, 101);
    //         if (_injuryChance <= _hardInjuryChance)
    //         {
    //             _camp._soldiers[_camp.SoldierInPlace - 1].InjuryType = 3;
    //             _camp._soldiers[_camp.SoldierInPlace - 1].InjuryTime = InjuryTimer[2];
    //         }
    //         else if (_injuryChance <= _mediumInjuryChance)
    //         {
    //             _camp._soldiers[_camp.SoldierInPlace - 1].InjuryType = 2;
    //             _camp._soldiers[_camp.SoldierInPlace - 1].InjuryTime = InjuryTimer[1];
    //         }
    //         else
    //         {
    //             _camp._soldiers[_camp.SoldierInPlace - 1].InjuryType = 1;
    //             _camp._soldiers[_camp.SoldierInPlace - 1].InjuryTime = InjuryTimer[0];
    //         }
    //         
    //         _camp._soldiers[_camp.SoldierInPlace - 1].FirstName = firstName;
    //         _camp._soldiers[_camp.SoldierInPlace - 1].LastName = lastName;
    //         _camp._soldiers[_camp.SoldierInPlace - 1].Age = age.ToString();
    //         _camp._soldiers[_camp.SoldierInPlace - 1].Situation = situation;
    //
    //         SoldierDebugText.text += $"{gameObject.name} : {firstName} {lastName}.\n";
    //         
    //         Debug.Log("Génération de soldat\nPrénom : " + firstName + "\nNom : " + lastName + "\nAge : " + age + "\nSituation : " + situation);
    //     }
    // }

    public void GeneratorSoldier() //Test New Generation
    {
        int _appearingChanceAppear = Random.Range(0, 101);
        Soldier soldierGenerate = null;

        foreach (var soldier in _camp._soldiers)
        {
            if (!soldier.isOccuped)
            {
                soldierGenerate = soldier;
                break;
            }
        }

        if (soldierGenerate != null && _appearingChanceAppear <= _appearingChance)
        {
            _camp._playerController.soldiers.Add(soldierGenerate);
            soldierGenerate.isOccuped = true;
            soldierGenerate.isCounted = true;
            _camp.SoldierInPlace++;
            string firstName = FirstName[Random.Range(0, FirstName.Count)];
            string lastName = LastName[Random.Range(0, LastName.Count)];
            string situation = Situation[Random.Range(0, Situation.Count)];
            int age = 0;

            int _militaryRankChance = Random.Range(0, 101);
            if (_militaryRankChance <= _captainClassChance)
            {
                soldierGenerate.MilitaryRank = "Rank : " + MilitaryRank[2];
                age = Random.Range(30, 36);
            }
            else if (_militaryRankChance <= _majorClassChance)
            {
                soldierGenerate.MilitaryRank = "Rank : " + MilitaryRank[1];
                age = Random.Range(25, 36);
            }
            else
            {
                soldierGenerate.MilitaryRank = "Rank : " + MilitaryRank[0];
                age = Random.Range(18, 36);
            }


            int _injuryChance = Random.Range(0, 101);
            if (_injuryChance <= _hardInjuryChance)
            {
                soldierGenerate.InjuryType = 3;
                soldierGenerate.InjuryTime = InjuryTimer[2];
            }
            else if (_injuryChance <= _mediumInjuryChance)
            {
                soldierGenerate.InjuryType = 2;
                soldierGenerate.InjuryTime = InjuryTimer[1];
            }
            else
            {
                soldierGenerate.InjuryType = 1;
                soldierGenerate.InjuryTime = InjuryTimer[0];
            }

            soldierGenerate.FirstName = firstName;
            soldierGenerate.LastName = lastName;
            soldierGenerate.Age = age.ToString();
            soldierGenerate.Situation = situation;

            SoldierDebugText.text += $"{gameObject.name} : {firstName} {lastName}.\n";

            Debug.Log("Génération de soldat\nPrénom : " + firstName + "\nNom : " + lastName + "\nAge : " + age +
                      "\nSituation : " + situation);
        }
    }
}