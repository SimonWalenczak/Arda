using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UICarController : MonoBehaviour
{
    [SerializeField] private NightManager _nightManager;
    [SerializeField] List<GameObject> wheelsMesh;
    [SerializeField] GameObject _bodyCar;
    [SerializeField] GameObject _directionSystem;

    private Quaternion rotation;
    public Transform target;

    [Header("Visual")] 
    [SerializeField] private TextMeshProUGUI _wheelsTypeText;
    [SerializeField] private TextMeshProUGUI _bodyCarTypeText;
    [SerializeField] private TextMeshProUGUI _directionSystemText;
    
    private void Update()
    {
        Vector3 relativePos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);        
        
        transform.localRotation = Quaternion.Slerp(transform.localRotation, rotation, Time.deltaTime/2);

        switch (_nightManager.wheelsType)
        {
            case 0:
                _wheelsTypeText.SetText("Soft");
                break;
            case 1:
                _wheelsTypeText.SetText("Medium");
                break;
            case 2:
                _wheelsTypeText.SetText("Hard");
                break;
        }
        foreach (var wheel in wheelsMesh)
        {
            switch (_nightManager.wheelsType)
            {
                case 0:
                    wheel.GetComponent<Renderer>().material.color = Color.green;
                    break;
                case 1:
                    wheel.GetComponent<Renderer>().material.color = Color.yellow;
                    break;
                case 2:
                    wheel.GetComponent<Renderer>().material.color = Color.red;
                    break;
            }
        }

        switch (_nightManager.bodyCarType)
        {
            case 0:
                _bodyCar.GetComponent<Renderer>().material.color = Color.green;
                _bodyCarTypeText.SetText("soft");
                break;
            case 1:
                _bodyCar.GetComponent<Renderer>().material.color = Color.yellow;
                _bodyCarTypeText.SetText("medium"); 
                break;
            case 2:
                _bodyCar.GetComponent<Renderer>().material.color = Color.red;
                _bodyCarTypeText.SetText("hard");
                break;
        }
        
        switch (_nightManager.directionSystemType)
        {
            case 0:
                _directionSystem.GetComponent<Renderer>().material.color = Color.green;
                _directionSystemText.SetText("soft");
                break;
            case 1:
                _directionSystem.GetComponent<Renderer>().material.color = Color.yellow;
                _directionSystemText.SetText("medium");
                break;
            case 2:
                _directionSystem.GetComponent<Renderer>().material.color = Color.red;
                _directionSystemText.SetText("hard");
                break;
        }
    }
}
