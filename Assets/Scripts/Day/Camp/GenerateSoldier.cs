using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateSoldier : MonoBehaviour
{
    public List<Sprite> FaceUpSprites;
    public List<Sprite> FaceDownSprites;
    public List<Sprite> BodySprites;
    public List<Color> FaceDownColor;

    public Tent tent;

    public List<SoldierInfo> Soldiers;
    
    private void Start()
    {
        tent = GetComponent<Tent>();
        Soldiers = tent.Soldiers;
        
        GeneratorSoldier();
    }

    public void GeneratorSoldier()
    {
        for(int i = 0; i < Soldiers.Count; i++)
        {
            Sprite faceUp = FaceUpSprites[Random.Range(0, FaceUpSprites.Count)];
            Sprite faceDown = FaceDownSprites[Random.Range(0, FaceDownSprites.Count)];
            Color bodyHairColor = FaceDownColor[Random.Range(0, FaceDownColor.Count)];

            Soldiers[i].FaceUp = faceUp;
            Soldiers[i].FaceDown = faceDown;
            Soldiers[i].FaceDownColor = bodyHairColor;

            switch (Soldiers[i].Rank)
            {
                case MilitaryRank.Génie:
                    Soldiers[i].Body = BodySprites[0];
                    break;
                case MilitaryRank.Officier:
                    Soldiers[i].Body = BodySprites[1];
                    break;
                case MilitaryRank.SecondeClasse:
                    Soldiers[i].Body = BodySprites[2];
                    break;
            }
            
        }
    }
}
