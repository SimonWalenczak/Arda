using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class BombingZone : MonoBehaviour
{
    #region Init. Variables

    private MeshCollider _meshCollider;

    private float minXPoint;
    private float maxXPoint;

    private float minZPoint;
    private float maxZPoint;

    private int Area;

    [SerializeField] int minY;
    [SerializeField] int maxY;
    [SerializeField] GameObject bomb;
    private int nbBomb;
    [SerializeField] private float ratioBomb;
    [SerializeField] private Terrain _terrain;
    [SerializeField] private Color color;

    [SerializeField] GameObject mainCamera;

    // [Header("Debug")] public GameObject pointsCheck;

    #endregion

    private void Start()
    {
        _meshCollider = GetComponent<MeshCollider>();
        GetComponent<MeshRenderer>().material.color = color;
        Initialize();
    }

    void Initialize()
    {
        minXPoint = _meshCollider.bounds.min.x;
        maxXPoint = _meshCollider.bounds.max.x;

        minZPoint = _meshCollider.bounds.min.z;
        maxZPoint = _meshCollider.bounds.max.z;

        print("min X : " + _meshCollider.bounds.min.x);
        print("max X : " + _meshCollider.bounds.max.x);
        print("min Z : " + _meshCollider.bounds.min.z);
        print("max Z : " + _meshCollider.bounds.max.z);

        float distX = math.abs(minXPoint - maxXPoint);
        float distZ = math.abs(minZPoint - maxZPoint);

        nbBomb = 0;
        Area = (int) (distX * distZ);

        print(Area);


        // var i = Instantiate(pointsCheck, new Vector3(minXPoint, 1, minZPoint), quaternion.identity);
        // i.name = "a";
        // i = Instantiate(pointsCheck, new Vector3(maxXPoint, 1, maxZPoint), quaternion.identity);
        // i.name = "b";
        // i = Instantiate(pointsCheck, new Vector3(minXPoint, 1, maxZPoint), quaternion.identity);
        // i.name = "c";
        // i = Instantiate(pointsCheck, new Vector3(maxXPoint, 1, minZPoint), quaternion.identity);
        // i.name = "d";

        // currentTimer = timerReset;
    }

    private void Update()
    {
        if (nbBomb < Area / (ratioBomb * 100))
                StartBombing();
    }

    void StartBombing()
    {
        float xPos = Random.Range(minXPoint, maxXPoint);
        float zPos = Random.Range(minZPoint, maxZPoint);
        float yPos = Random.Range(minY, maxY);

        Vector3 spawnPos = new Vector3(xPos, transform.position.y + yPos, zPos);

        RaycastHit hit;
        if (Physics.Raycast(spawnPos, -Vector3.up, out hit))
        {
            Debug.DrawLine(transform.position, hit.point, Color.cyan);

            if (hit.collider.gameObject.name == name)
            {
                GameObject actualBomb = Instantiate(bomb, spawnPos, Quaternion.Euler(90f, 0f, 0f));
                actualBomb.GetComponent<Bomb>().MainCamera = mainCamera;
                nbBomb++;
            }
        }
    }
}