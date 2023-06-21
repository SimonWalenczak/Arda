using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Screenshot : MonoBehaviour
{
    [SerializeField]
    private string path;
    [SerializeField]
    [Range(1, 5)]
    private int size = 1;


    void Update()
    {
        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            path += "screenshot ";
            path += System.Guid.NewGuid().ToString() + ".png";

            ScreenCapture.CaptureScreenshot(path, size);
            Debug.Log("screenshottuuuu");
        }
    }
}
