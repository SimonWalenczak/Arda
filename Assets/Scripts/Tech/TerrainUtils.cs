using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainUtils : MonoBehaviour
{
    public static TerrainUtils Instance;
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static (int x, int y) GetTerrainCoordsFromWorldPosition(Terrain terrain, Vector3 worldPosition)
    {
        var terrainPosition = terrain.transform.position;
        TerrainData terrainData = terrain.terrainData;
        var terrainSize = terrainData.size;
        float relativeHitTerX = (worldPosition.x - terrainPosition.x) / terrainSize.x;
        float relativeHitTerZ = (worldPosition.z - terrainPosition.z) / terrainSize.z;

        float relativeTerCoordX = terrainData.heightmapResolution * relativeHitTerX;
        float relativeTerCoordZ = terrainData.heightmapResolution * relativeHitTerZ;

        int hitPointTerX = Mathf.FloorToInt(relativeTerCoordX);
        int hitPointTerZ = Mathf.FloorToInt(relativeTerCoordZ);

        // Yes, Z as X, X as Y
        return (hitPointTerZ, hitPointTerX);
    }
}
