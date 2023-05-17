using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerChecker : MonoBehaviour
{

    public CanvasGroup ControllerOff;

    private void Awake()
    {
        ControllerOff.alpha = 0;
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (Gamepad.current == null)
        {
            Time.timeScale = 0;
            ControllerOff.alpha = 1;
        }
        else
        {
            Time.timeScale = 1;
            ControllerOff.alpha = 0;
        }
    }
}
