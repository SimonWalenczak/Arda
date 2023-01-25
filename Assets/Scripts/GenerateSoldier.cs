using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateSoldier : MonoBehaviour
{
    public List<String> LastName;
    public List<String> FirstName;
    public List<int> Age;
    public List<String> Situation;
    
    enum States
    {
        green,
        orange,
        red
    }
    public Enum state;
    
    [SerializeField] private float _appearingChance;

    public void GeneratorSoldier()
    {
        int _appearingChanceAppear = Random.Range(0, 101);
        if (_appearingChanceAppear < _appearingChance)
        {
            string firstName = FirstName[Random.Range(0, FirstName.Count)];
            string lastName = LastName[Random.Range(0, LastName.Count)];
            int age = Age[Random.Range(0, Age.Count)];
            string situation = Situation[Random.Range(0, Situation.Count)];
            
            Debug.Log("Génération de soldat\nPrénom : " + firstName + "\nNom : " + lastName + "\nAge : " + age + "\nSituation : " + situation);
        }
    }
}
