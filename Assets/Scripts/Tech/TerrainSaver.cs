using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSaver : MonoBehaviour
{
    public List<Terrain> Terrains;
    List<float[,]> dataSaver = new List<float[,]>();

    [System.Obsolete]
    private void Start()
    {
        for (int i = 0; i < Terrains.Count; i++)
        {
            //Debug.Log(Terrains[i]);
            //Debug.Log(Terrains[i].terrainData.GetHeights(0, 0, Terrains[i].terrainData.heightmapWidth, Terrains[i].terrainData.heightmapHeight));
            dataSaver.Add(Terrains[i].terrainData.GetHeights(0, 0, Terrains[i].terrainData.heightmapWidth, Terrains[i].terrainData.heightmapHeight));
        }
    }

    private void OnApplicationQuit()
    {
        for (int i = 0; i < Terrains.Count; i++)
        {
            Terrains[i].terrainData.SetHeights(0, 0, dataSaver[i]);
        }
    }
}
