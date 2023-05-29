using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
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
    [SerializeField] private int _nbBombMax;
    [SerializeField] private Terrain _terrain;
    [SerializeField] private Color color;

    [SerializeField] GameObject mainCamera;

    [SerializeField] private float startBombingHour;
    [SerializeField] private float startBombingMinute;
    [SerializeField] private float stopBombingHour;
    [SerializeField] private float stopBombingMinute;
    [SerializeField] private bool _canBombing;

    private float _hourDuration;
    private float _minuteDuration;
    private float _bombingDuration;
    private double _bombPerSec;
    [SerializeField] private int nbBomb;

    private float _currentTime = 0;

    // [Header("Debug")] public GameObject pointsCheck;

    #endregion

    private void Start()
    {
        _meshCollider = GetComponent<MeshCollider>();
        GetComponent<MeshRenderer>().material.color = color;
        Initialize();

        _hourDuration = stopBombingHour - startBombingHour;
        _minuteDuration = stopBombingMinute - startBombingMinute;

        _bombingDuration = (_hourDuration * 3600 + _minuteDuration * 60) / DayManager.Instance.TimeMultiplier;
        _bombPerSec = Math.Ceiling(_nbBombMax / _bombingDuration);
        // print(_bombingDuration);
        // print(_bombPerSec);

        if (DayManager.Instance._isTuto == false)
            _canBombing = true;
        else
        {
            //Condition si le joueur sort de la première tente.
        }
    }

    void Initialize()
    {
        minXPoint = _meshCollider.bounds.min.x;
        maxXPoint = _meshCollider.bounds.max.x;

        minZPoint = _meshCollider.bounds.min.z;
        maxZPoint = _meshCollider.bounds.max.z;

        //print("min X : " + _meshCollider.bounds.min.x);
        //print("max X : " + _meshCollider.bounds.max.x);
        //print("min Z : " + _meshCollider.bounds.min.z);
        //print("max Z : " + _meshCollider.bounds.max.z);

        float distX = math.abs(minXPoint - maxXPoint);
        float distZ = math.abs(minZPoint - maxZPoint);

        nbBomb = 0;
        Area = (int) (distX * distZ);

        //print(Area);


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
        if (_canBombing)
        {
            if (DayManager.Instance.CurrentHour >= startBombingHour &&
                DayManager.Instance.CurrentMinute >= startBombingMinute)
            {
                //print("start bombing");
                _currentTime += Time.deltaTime;

                if (_currentTime >= 1)
                {
                    //print("currentTime = 1");
                    for (int i = 0; i < _bombPerSec; i++)
                    {
                        //print("i < bombePerSec");
                        if (nbBomb < _nbBombMax)
                            StartBombing();
                    }

                    _currentTime = 0;
                }
            }

            if (DayManager.Instance.CurrentHour >= stopBombingHour &&
                DayManager.Instance.CurrentMinute >= stopBombingMinute)
            {
                //print("stop bombing");
            }

            if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                //print(_bombPerSec);
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
                nbBomb++;
                //print("bomb create");
            }
            else
            {
                StartBombing();
            }
        }
    }
}