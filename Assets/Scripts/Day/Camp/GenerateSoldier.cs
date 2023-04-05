using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateSoldier : MonoBehaviour
{
    public List<Sprite> FaceSprites;
    public List<Sprite> BodyHairSprites;
    public List<Color> BodyHairColor;

    private Camp _camp;
    private void Start()
    {
        _camp = GetComponent<Camp>();
    }

    public void GeneratorSoldier()
    {
        foreach (var soldier in _camp._soldiers)
        {
            Color bodyHairColor = BodyHairColor[Random.Range(0, BodyHairColor.Count)];
            Sprite face = FaceSprites[Random.Range(0, FaceSprites.Count)];
            Sprite bodyHair = BodyHairSprites[Random.Range(0, BodyHairSprites.Count)];

            soldier.Face = face;
            soldier.BodyHair = bodyHair;
            soldier.BodyHairColor = bodyHairColor;

            soldier.IsDiagnosed = false;
        }
    }
}
