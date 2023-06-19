using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkyboxMeteo : MonoBehaviour
{
    public Material Skybox;
    [SerializeField] float rotationSpeed;
    float rotationValue = 0;

    [Space(10)]
    [Header("Start")]
    [SerializeField] float startIntensity;

    [Space(10)]
    [Header("Half Day")]
    [SerializeField] float halfIntensity;
    [SerializeField] private float halfHour;
    [SerializeField] private float halfMinute;

    [Space(10)]
    [Header("End")]
    [SerializeField] float endIntensity;
    [SerializeField] private float fadeEndHour;
    [SerializeField] private float fadeEndMinute;


    private void Start()
    {
        Skybox.DOFloat(halfIntensity, "_Exposure", 10f);   
    }

    void Update()
    {
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
