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
    public string MilitaryRank;

    //public int TotalBullet;
    public int NbBulletBust;
    public int NbBulletArmLeft;
    public int NbBulletArmRight;
    public int NbBulletLegLeft;
    public int NbBulletLegRight;

    //public bool IsAlived;

    public Sprite Face;
    public Sprite BodyHair;
    public Color BodyHairColor;
}
