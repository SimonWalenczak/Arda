using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class BombingOnPlayer : MonoBehaviour
{
    private MeshCollider _meshCollider;

    private float minXPoint;
    private float maxXPoint;

    private float minZPoint;
    private float maxZPoint;

    private int Area;

    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] int minY;
    [SerializeField] int maxY;
    [SerializeField] GameObject bomb;

    [SerializeField] GameObject mainCamera;

    [SerializeField] private bool _canBombing;

    [SerializeField] double _bombPerSec;
    private float _currentTime = 0;

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

        float distX = math.abs(minXPoint - maxXPoint);
        float distZ = math.abs(minZPoint - maxZPoint);

        Area = (int)(distX * distZ);
    }

    private void Update()
    {
        if (_canBombing)
        {
            _currentTime += Time.deltaTime;

            if (_currentTime >= 1)
            {
                for (int i = 0; i < _bombPerSec; i++)
                {
                    StartBombing();
                }

                _currentTime = 0;
            }
        }
    }

    void StartBombing()
    {
        //print("Try Bombing");
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
            }
            else
            {
                StartBombing();
            }
        }
    }

    public static bool Contains(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Contains(_playerLayer, other.gameObject.layer))
        {
            _canBombing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Contains(_playerLayer, other.gameObject.layer))
        {
            _canBombing = false;
        }
    }
}