using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GeneralDialog : MonoBehaviour
{
    public bool CanTalk;
    public bool IsFinish;

    [SerializeField] private TextMeshProUGUI _generalTextVisual;
    [SerializeField] private List<Dialog> _generalText;
    private int index = 0;
    private int _totalSoldierDead;
    private int _totalSoldierSaved;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && index < _generalText.Count)
            index++;
        
        _generalTextVisual.SetText(_generalText[index].DialogText);
    }
}
