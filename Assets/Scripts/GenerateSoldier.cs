using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    public int InjuryTimerMax;
    public List<float> InjuryTimer;

    private Camp _camp;

    public TextMeshProUGUI SoldierDebugText;

    private void Start()
    {
        _camp = GetComponent<Camp>();
        SoldierDebugText.text = "";
        InjuryTimer[0] = InjuryTimerMax;
        InjuryTimer[1] = InjuryTimerMax * 0.7f;
        InjuryTimer[2] = InjuryTimerMax * 0.4f;
    }

    public void GeneratorSoldier()
    {
        int _appearingChanceAppear = Random.Range(0, 101);

        foreach (var soldier in _camp._soldiers)
        {
            if (!soldier.isOccuped && _appearingChanceAppear <= _appearingChance)
            {
                soldier.isOccuped = true;
                _camp.SoldierInPlace++;
                string firstName = FirstName[Random.Range(0, FirstName.Count)];
                string lastName = LastName[Random.Range(0, LastName.Count)];
                string situation = Situation[Random.Range(0, Situation.Count)];
                int age = 0;

                int _militaryRankChance = Random.Range(0, 101);
                if (_militaryRankChance <= _captainClassChance)
                {
                    soldier.MilitaryRank = "Rank : " + MilitaryRank[2];
                    age = Random.Range(30, 36);
                }
                else if (_militaryRankChance <= _majorClassChance)
                {
                    soldier.MilitaryRank = "Rank : " + MilitaryRank[1];
                    age = Random.Range(25, 36);
                }
                else
                {
                    soldier.MilitaryRank = "Rank : " + MilitaryRank[0];
                    age = Random.Range(18, 36);
                }


                int _injuryChance = Random.Range(0, 101);
                if (_injuryChance <= _hardInjuryChance)
                {
                    soldier.InjuryType = 3;
                }
                else if (_injuryChance <= _mediumInjuryChance)
                {
                    soldier.InjuryType = 2;
                }
                else
                {
                    soldier.InjuryType = 1;
                }

                soldier.InjuryTime = InjuryTimer[0];
                soldier.LifeTimeStep = soldier.InjuryTime;
                soldier.InjuryTimeUnit = soldier.InjuryTime / 10;
                soldier.InjuryTypeOrigin = soldier.InjuryType;

                soldier.FirstName = firstName;
                soldier.LastName = lastName;
                soldier.Age = age.ToString();
                soldier.Situation = situation;

                SoldierDebugText.text += $"{gameObject.name} : {firstName} {lastName}.\n";

                Debug.Log("Génération de soldat\nPrénom : " + firstName + "\nNom : " + lastName + "\nAge : " + age +
                          "\nSituation : " + situation);


                _camp._arcadeCar.GetComponent<PlayerController>().soldiers.Add(soldier);
                break;
            }
        }
    }
}