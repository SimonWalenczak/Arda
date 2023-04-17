using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulletCreation : MonoBehaviour
{
    private float minXPoint;
    private float maxXPoint;

    private float minZPoint;
    private float maxZPoint;
    
    private int Area;

    private BoxCollider collider;

    public GameObject bullet;

    private void Start()
    {
        collider = GetComponent<BoxCollider>();
        Initialize();
    }

    void Initialize()
    {
        minXPoint = collider.bounds.min.x;
        maxXPoint = collider.bounds.max.x;

        minZPoint = collider.bounds.min.z;
        maxZPoint = collider.bounds.max.z;

        print("min X : " + collider.bounds.min.x);
        print("max X : " + collider.bounds.max.x);
        print("min Z : " + collider.bounds.min.z);
        print("max Z : " + collider.bounds.max.z);

        float distX = math.abs(minXPoint - maxXPoint);
        float distZ = math.abs(minZPoint - maxZPoint);
        
        CreateBullet();
    }

    public void CreateBullet()
    {
        float xPos = Random.Range(minXPoint, maxXPoint);
        float zPos = Random.Range(minZPoint, maxZPoint);
        
        Vector3 spawnPos = new Vector3(xPos, transform.position.y, zPos);
        
        RaycastHit hit;
        if (Physics.Raycast(spawnPos, -Vector3.up, out hit))
        {
            Debug.DrawLine(transform.position, hit.point, Color.cyan);

            if (hit.collider.gameObject.name == name)
            {
                GameObject actualbullet = Instantiate(bullet, spawnPos, Quaternion.Euler(90f, 0f, 0f));
                Debug.Log("bullet create");
            }
            Debug.Log("nope ");
        }
    }
}
