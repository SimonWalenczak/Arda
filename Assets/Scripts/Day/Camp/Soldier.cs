using System;
using System.Collections;
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
    public bool isDiagnosed;
    public int index;

    public string LastName;
    public string FirstName;
    public string Age;
    public string Situation;
    public string MilitaryRank;
    public Sprite Face;
    public Sprite BodyHair;
    public Color BodyHairColor;
    public int InjuryTypeOrigin;
    public int InjuryType;

    public float InjuryTime;
    public float InjuryTimeUnit;
    public int UnitToSec;
    
    public GameObject LifeBarParent;
    public Image LifeBar;
    public Camera cam;

    public float LifeTimeStep;

    public bool _isDying;

    private void Update()
    {
        if (isOccuped && !_isDying)
        {
            StartCoroutine(Dying());
        }
    }

    IEnumerator Dying()
    {
        _isDying = true;
        yield return new WaitForSeconds(UnitToSec);
        InjuryTime -= InjuryTimeUnit;
        _isDying = false;
    }
}