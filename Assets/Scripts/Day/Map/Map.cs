using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject panalMap;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            panalMap.SetActive(!panalMap.activeSelf);
        }
    }
}