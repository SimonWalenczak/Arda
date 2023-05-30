using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public enum TextType
{
    NoType,
    EndRadiologie,
    End
}

public class TextTutoManager : MonoBehaviour
{
    public int index;

    [SerializeField] private TMP_Text textTuto;

    public List<TextTuto> _textTutos;

    [SerializeField] private GameObject _blackScreen;
    
    private void Start()
    {
        index = 1;
        _blackScreen.SetActive(true);
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            if (index < _textTutos.Count -1)
            {
                index++;
            }
            else
            {
                _blackScreen.SetActive(false);
                textTuto.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }
      
        
        textTuto.text = _textTutos[index].text;
    }
}
