using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulletCreation : MonoBehaviour
{
    [SerializeField] private Camp currentCamp;
    
    private float minXPoint;
    private float maxXPoint;

    private float minZPoint;
    private float maxZPoint;

    private BoxCollider _collider;

    public GameObject bullet;

    private void Start()
    {
        _collider = GetComponent<BoxCollider>();
        Initialize();
    }

    void Initialize()
    {
        minXPoint = _collider.bounds.min.x;
        maxXPoint = _collider.bounds.max.x;

        minZPoint = _collider.bounds.min.z;
        maxZPoint = _collider.bounds.max.z;

        float distX = math.abs(minXPoint - maxXPoint);
        float distZ = math.abs(minZPoint - maxZPoint);
    }
    

    public void CreateBullet()
    {
        float xPos = Random.Range(minXPoint, maxXPoint);
        float zPos = Random.Range(minZPoint, maxZPoint);
        
        Vector3 spawnPos = new Vector3(xPos, transform.position.y+0.1f, zPos);
        
        RaycastHit hit;
        if (Physics.Raycast(spawnPos, -Vector3.up, out hit))
        {
            Debug.DrawLine(transform.position, hit.point, Color.cyan);

            if (hit.collider.gameObject.name == name)
            {
                GameObject actualbullet = Instantiate(bullet, spawnPos, Quaternion.Euler(-90f, 0f, 0f));
                actualbullet.transform.SetParent(currentCamp.transform);
                currentCamp.ActualBullets.Add(actualbullet);
            }
            else
            {
                CreateBullet();
            }
        }
    }
}
