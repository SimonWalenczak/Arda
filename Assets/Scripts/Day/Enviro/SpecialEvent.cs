using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class SpecialEvent : MonoBehaviour
{
    [SerializeField] TimeController _timeController;

    public float startEventHour;
    public float endEventHour;

    private TimeSpan startEventTime;
    private TimeSpan endEventTime;

    [SerializeField] private bool _isBombing;

    [SerializeField] private GameObject bomb;
    public Terrain terrain;


    private int nbBomb = 0;
    private float currentTimer;
    public float TimerReset;

    private float x, y;
    
    
    public BoxCollider2D spawnArea;
    Vector2 maxSpawnPos;

    private void Start()
    {
        spawnArea = GetComponent<BoxCollider2D>();
        spawnArea.enabled = false; 
        maxSpawnPos = new Vector2(spawnArea.size.x / 2, spawnArea.size.y / 2);
        
        
        startEventTime = TimeSpan.FromHours(startEventHour);
        currentTimer = TimerReset;
    }

    private void Update()
    {
        if (_timeController._currentTime >= DateTime.Now.Date + TimeSpan.FromHours(startEventHour) &&
            _isBombing == false)
        {
            currentTimer -= Time.deltaTime;
            if (currentTimer <= 0)
            {
                StartBombing();
            }
        }
    }

    public void StartBombing()
    {
        RaycastHit hit;

        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        // Vector3 pos = new Vector3(Random.Range(-maxSpawnPos.x, maxSpawnPos.x) % spawnArea.size.x, Random.Range(-maxSpawnPos.y, maxSpawnPos.y) % spawnArea.size.y, 0);
 
    }

    private void Bombing()
    {
        var actualBomb = Instantiate(bomb,
            new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), quaternion.identity);
        actualBomb.GetComponent<Bomb>().terrain = terrain;
    }
}