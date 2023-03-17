using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] GameObject bomb;
    [SerializeField] float timer;
    [SerializeField] float spawnRadius;
    [SerializeField] int quantity;

    float timerCount = 0;

    Vector3 spawnPos;

 
    void Update()
    {
        timerCount += Time.deltaTime;

        if(timerCount >= timer)
        {
            for (int i = 0; i < quantity; i++)
            {
                float xPos = Random.Range(0, spawnRadius);
                float yPos = Random.Range(0, spawnRadius);
                spawnPos = new Vector3(transform.position.x + xPos, transform.position.y + yPos, transform.position.z);
                Instantiate(bomb, spawnPos, transform.rotation);
            }
            
            timerCount = 0;
        }
    }
}
