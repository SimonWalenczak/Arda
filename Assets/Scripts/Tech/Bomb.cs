using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [HideInInspector] public Terrain terrain;
    [SerializeField] GameObject explosionVfx;
    private TerrainData terrainData;

    [SerializeField] float DetectionDist;
    [SerializeField] int GroundLayerNb = 6;

    //int xBase = 0;
    //int yBase = 0;

    bool hasExploded = false;


    
    [System.Obsolete]
    private void Start()
    {
        //EditTerrain();
    }

    [System.Obsolete]
    void EditTerrain(int xBase, int yBase)
    {
        int xRes = 4; //terrainData.heightmapWidth;
        int yRes = 4; //terrainData.heightmapHeight;

        //tests made with value of 50, 50 for base values

        float[,] Heights = terrainData.GetHeights(xBase - 1, yBase - 1, xRes, yRes);

        for (int y = 0; y < yRes; y++)
        {
            for (int x = 0; x < xRes; x++)
            {
                float cos = Mathf.Cos(x);
                float sin = Mathf.Sin(y);

                //Debug.Log(cos - sin);

                //Heights[x, y] = (cos - sin) / 350;
                Heights[x, y] = (cos - sin) / 350;
                //Heights[x, y] = 1;
            }
        }

        terrainData.SetHeights(xBase - 1, yBase - 1, Heights);
    }

    [System.Obsolete]
    void FixedUpdate()
    {
        if (transform.position.y < -100)
        {
            Destroy(gameObject);
        }
        
        int layerMask = 1 << GroundLayerNb;

        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        Debug.DrawRay(transform.position, fwd * DetectionDist, Color.yellow, layerMask);

        RaycastHit hit;

        if (Physics.Raycast(transform.position, fwd, out hit, DetectionDist, layerMask))
        {
            //print("There is something in front of the object!");

            terrain = hit.collider.GetComponent<Terrain>();
            terrainData = terrain.terrainData;

            int xPos = TerrainUtils.GetTerrainCoordsFromWorldPosition(terrain, hit.point).y;
            int yPos = TerrainUtils.GetTerrainCoordsFromWorldPosition(terrain, hit.point).x;

            if (!hasExploded)
            {
                Instantiate(explosionVfx, transform.position, transform.rotation);
                EditTerrain(xPos, yPos);
                hasExploded = true;
                Destroy(gameObject);
            }
        }

    }
}
