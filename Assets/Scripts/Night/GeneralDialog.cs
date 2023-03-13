using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GeneralDialog : MonoBehaviour
{
    public bool CanTalk;
    public bool CanUpgrade;

    [SerializeField] private TextMeshProUGUI _generalTextVisual;
    [SerializeField] private List<Dialog> _generalText;
    private int index = 0;
    private int _totalSoldierDead;
    private int _totalSoldierSaved;

    [SerializeField] private GameObject BilanPerte;
    [SerializeField] private GameObject BilanSauve;

    private void Update()
    {
        if (CanTalk)
        {
            _generalTextVisual.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F) && index < _generalText.Count-1)
                index++;

            _generalTextVisual.SetText(_generalText[index].DialogText);
        }

        switch (index + 1)
        {
            case 0:
                BilanPerte.SetActive(false);
                BilanSauve.SetActive(false);
                break;
            
            case 2:
                BilanPerte.SetActive(true);
                break;
            
            case 3:
                BilanPerte.SetActive(false);
                break;
            
            case 4:
                BilanSauve.SetActive(true);
                break;  
            
            case 5:
                BilanSauve.SetActive(false);
                break;
        }
    }
}