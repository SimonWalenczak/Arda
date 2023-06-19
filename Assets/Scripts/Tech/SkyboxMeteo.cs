using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkyboxMeteo : MonoBehaviour
{
    public Material Skybox;
    [SerializeField] float rotationSpeed;
    [SerializeField] float risingSpeed;
    [SerializeField] float nightfallSpeed;
    float rotationValue = 0;

    [Space(10)]
    [Header("Start")]
    [SerializeField] float startIntensity;

    [Space(10)]
    [Header("Half Day")]
    [SerializeField] float halfIntensity;

    [Space(10)]
    [Header("Nightfall")]
    [SerializeField] float endIntensity;
    [SerializeField] private int fadeEndHour;
    [SerializeField] private int fadeEndMinute;


    private void Start()
    {
        Skybox.DOFloat(halfIntensity, "_Exposure", risingSpeed);   
    }

    void Update()
    {
        if (DayManager.Instance.CurrentHour == fadeEndHour && DayManager.Instance.CurrentMinute == fadeEndMinute)
        {
            Skybox.DOFloat(endIntensity, "_Exposure", nightfallSpeed);
        }


        rotationValue += rotationSpeed * Time.deltaTime;
        if (rotationValue >= 360)
        {
            rotationValue = 0;
        }
        Skybox.SetFloat("_Rotation", rotationValue);
    }

    private void OnApplicationQuit()
    {
        Skybox.SetFloat("_Exposure", startIntensity);
        Skybox.SetFloat("_Rotation", 0);
    }
}
