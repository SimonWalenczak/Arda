using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateSoldier : MonoBehaviour
{
    public List<String> LastName;
    public List<String> FirstName;
    public List<String> Situation;
    
    enum States
    {
        green,
        orange,
        red
    }
    public Enum state;
    [SerializeField] private float _appearingChance;
    
    private Camp _camp;
    private void Start()
    {
        _camp = GetComponent<Camp>();
    }

    public void GeneratorSoldier()
    {
        int _appearingChanceAppear = Random.Range(0, 101);
        if (_camp.SoldierInPlace < 8 && _appearingChanceAppear <= _appearingChance)
        {
            _camp.SoldierInPlace++;
            string firstName = FirstName[Random.Range(0, FirstName.Count)];
            string lastName = LastName[Random.Range(0, LastName.Count)];
            int age = Random.Range(18,36);
            string situation = Situation[Random.Range(0, Situation.Count)];
            
            //Visual
            _camp._soldierCard[_camp.SoldierInPlace-1].FirstNameText.SetText(firstName);
            _camp._soldierCard[_camp.SoldierInPlace-1].LastNameText.SetText(lastName);
            _camp._soldierCard[_camp.SoldierInPlace-1].AgeText.SetText(age.ToString());
            _camp._soldierCard[_camp.SoldierInPlace-1].SituationText.SetText(situation);
            
            Debug.Log("Génération de soldat\nPrénom : " + firstName + "\nNom : " + lastName + "\nAge : " + age + "\nSituation : " + situation);
        }
    }
}