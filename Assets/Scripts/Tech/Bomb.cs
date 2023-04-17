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

    [HideInInspector] public GameObject MainCamera; 

    //int xBase = 0;
    //int yBase = 0;

    bool hasExploded = false;

    int goldenNumber = 1000;

    
    [System.Obsolete]
    private void Start()
    {
        //EditTerrain();
    }

    [System.Obsolete]
    void EditTerrain(int xBase, int yBase)
    {
        int xRes = 4;
        int yRes = 4;
        //int xRes = terrainData.heightmapWidth;
        //int yRes = terrainData.heightmapHeight;

        int smoothness = 850;

        //tests made with value of 50, 50 for base values

        float[,] Heights = terrainData.GetHeights(xBase - 1, yBase - 1, xRes, yRes);

        for (int y = 0; y < yRes; y++)
        {
            for (int x = 0; x < xRes; x++)
            {
                float cos = Mathf.Cos(x);
                float sin = Mathf.Sin(y);

                //Debug.Log(cos - sin);

                Heights[x, y] = Heights[x,y] + ((cos - sin) - 1) / smoothness;
                //Heights[x, y] = ((cos - sin)-1) / 350;
                //Heights[x, y] = cos / 350;
            }
        }

        terrainData.SetHeights(xBase - 1, yBase - 1, Heights);
    }


    void DestroyProps()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 5, transform.forward);
        //print("ouai c moi");

        foreach (RaycastHit hit in hits)
        {
            if(hit.collider.tag == "DestructibleObj")
            {
                //print("cc");
                hit.collider.gameObject.GetComponent<Destructible>().Destruction();
            }
        }
    }


    void TriggerCameraShake()
    {
        float dist = Vector3.Distance(gameObject.transform.position, MainCamera.transform.position);
        if (dist <= 75)
        {
            CMvcamShale.Instance.ShakeCamera(3f, 1.25f);
        }

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

        Debug.DrawRay(transform.position, fwd * DetectionDist, Color.yellow, 3);

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
                Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y-2, transform.position.z);  
                Instantiate(explosionVfx, spawnPos, transform.rotation);
                DestroyProps();
                TriggerCameraShake();
                EditTerrain(xPos, yPos);
                hasExploded = true;
                Destroy(gameObject);
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5);
    }
}
