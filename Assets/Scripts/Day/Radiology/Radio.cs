using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Radio : MonoBehaviour
{
    [SerializeField] private Camera cam;
    
    void Update()
    {
        Vector2 cursorPos = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(cursorPos.x, cursorPos.y);
    }
}