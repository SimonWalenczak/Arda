using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateSoldier : MonoBehaviour
{
    public List<Sprite> FaceSprites;
    public List<Sprite> BodyHairSprites;
    public List<Color> BodyHairColor;

    public List<SoldierStruct> Soldiers = new List<SoldierStruct>();
    
    private void Start()
    {
        GeneratorSoldier();
    }

    public void GeneratorSoldier()
    {
        for(int i = 0; i < Soldiers.Count; i++)
        {
            Color bodyHairColor = BodyHairColor[Random.Range(0, BodyHairColor.Count)];
            Sprite face = FaceSprites[Random.Range(0, FaceSprites.Count)];
            Sprite bodyHair = BodyHairSprites[Random.Range(0, BodyHairSprites.Count)];

            SoldierStruct newSoldier = Soldiers[i];
            newSoldier.Face = face;
            newSoldier.BodyHair = bodyHair;
            newSoldier.BodyHairColor = bodyHairColor;
            Soldiers[i] = newSoldier;
        }
    }
}
