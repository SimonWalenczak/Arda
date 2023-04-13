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

    [HideInInspector] public Sprite Face;
    [HideInInspector]  public Sprite BodyHair;
    [HideInInspector] public Color BodyHairColor;
}