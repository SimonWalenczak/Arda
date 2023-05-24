using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class SoldierInfo
{
    public string Name;
    public string Age;
    public string Achievements;
    public enum MilitaryRank {Soldier, Officer, Engineer};
    public int MinutesConsumed;

    //public int TotalBullet;

    [Space(10)]
    [Header("0 = Torso ; 1 = Left Arm ; 2 = Right Arm ; 3 = Left Leg ; 4 = Right Leg")]
    public List<int> Bullets = new List<int>();


    //public int NbBulletTorso;
    //public int NbBulletArmLeft;
    //public int NbBulletArmRight;
    //public int NbBulletLegLeft;
    //public int NbBulletLegRight;

    //public bool IsAlived;

    public Sprite Face;
    public Sprite BodyHair;
    public Color BodyHairColor;
}
