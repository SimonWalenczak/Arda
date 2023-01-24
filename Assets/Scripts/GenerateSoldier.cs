using System;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSoldier : MonoBehaviour
{
    public List<String> Nom;
    public List<String> Prenom;
    public List<int> Age;
    public List<String> Situation;
    
    enum States
    {
        green,
        orange,
        red
    }
    public Enum state;
}
