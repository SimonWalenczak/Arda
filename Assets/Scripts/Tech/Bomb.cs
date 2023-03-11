using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] Terrain terrain;
    private TerrainData terrainData;

    [SerializeField] float DetectionDist;
    [SerializeField] int GroundLayerNb = 6;

    int xBase = 0;
    int yBase = 0;

    private void Awake()
    {
        terrainData = terrain.terrainData;
    }

    [System.Obsolete]
    private void Start()
    {
        EditTerrain();
    }

    [System.Obsolete]
    void EditTerrain()
    {
        int xRes = 4; //terrainData.heightmapWidth;
        int yRes = 4; // terrainData.heightmapHeight;

        float [,] Heights = terrainData.GetHeights(50, 50, xRes, yRes);

        for (int y = 0; y < yRes; y++)
        {
            for (int x = 0; x < xRes; x++)
            {
                float cos = Mathf.Cos(x);
                float sin = -Mathf.Sin(y);
                //Debug.Log(cos - sin);

                Heights[x, y] = (cos - sin) / 250;
            }
        }

        terrainData.SetHeights(50, 50, Heights);
    }

    void FixedUpdate()
    {

        int layerMask = 1 << GroundLayerNb;

        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        Debug.DrawRay(transform.position, fwd * DetectionDist, Color.yellow, layerMask);

        RaycastHit hit;

        if (Physics.Raycast(transform.position, fwd, out hit, DetectionDist, layerMask))
        {
            //print("There is something in front of the object!");

        }

    }
}
