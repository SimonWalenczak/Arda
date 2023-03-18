using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using Random = UnityEngine.Random;

public class SpecialEvent : MonoBehaviour
{
    private MeshCollider _meshCollider;

    private float minXPoint;
    private float maxXPoint;

    private float minZPoint;
    private float maxZPoint;

    private int Area;

    public GameObject pointsCheck;
    
    public int nbBomb;

    [SerializeField] GameObject bomb;
    [SerializeField] float timerReset;

    [SerializeField] float currentTimer = 0;

    private void Start()
    {
        _meshCollider = GetComponent<MeshCollider>();
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
        int Area = (int) (distX * distZ);

        print(Area);


        var i = Instantiate(pointsCheck, new Vector3(minXPoint, 1, minZPoint), quaternion.identity);
        i.name = "a";
        i = Instantiate(pointsCheck, new Vector3(maxXPoint, 1, maxZPoint), quaternion.identity);
        i.name = "b";
        i = Instantiate(pointsCheck, new Vector3(minXPoint, 1, maxZPoint), quaternion.identity);
        i.name = "c";
        i = Instantiate(pointsCheck, new Vector3(maxXPoint, 1, minZPoint), quaternion.identity);
        i.name = "d";

        currentTimer = timerReset;
    }

    private void Update()
    {
        currentTimer -= Time.deltaTime;
        StartBombing();
    }

    public LayerMask layerMask;

    void StartBombing()
    {
        if (currentTimer <= 0)
        {
            float xPos = Random.Range(minXPoint, maxXPoint);
            float zPos = Random.Range(minZPoint, maxZPoint);
            float yPos = Random.Range(2, 5);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.left, out hit,
                    Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(transform.position, Vector3.left, Color.yellow);

                if (hit.collider.name == gameObject.name)
                {
                    Vector3 spawnPos = new Vector3(transform.position.x + xPos, transform.position.y + yPos,
                        transform.position.z + zPos);

                    Instantiate(bomb, spawnPos, Quaternion.identity);

                    currentTimer = timerReset;
                    nbBomb++;
                }
            }
        }
    }
}