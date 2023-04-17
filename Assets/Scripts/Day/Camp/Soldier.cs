using UnityEngine;
using Color = UnityEngine.Color;

[System.Serializable]
public struct SoldierStruct
{
    public int Index;

    public string Name;
    public string Age;
    public string Achievements;
    public string MilitaryRank;

    public int TotalBullet;
    public int NbBulletBust;
    public int NbBulletArmLeft;
    public int NbBulletArmRight;
    public int NbBulletLegLeft;
    public int NbBulletLegRight;

    public bool IsAlived;
    
    [HideInInspector] public Sprite Face;
    [HideInInspector] public Sprite BodyHair;
    [HideInInspector] public Color BodyHairColor;
}