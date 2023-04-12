using UnityEngine;
using Color = UnityEngine.Color;

[System.Serializable]
public struct SoldierStruct
{
    public int Index;
    public bool IsDiagnosed;

    public string Name;
    public string Age;
    public string Achievements;
    public string MilitaryRank;

    public Sprite Face;
     public Sprite BodyHair;
     public Color BodyHairColor;
}