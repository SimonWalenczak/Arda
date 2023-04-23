using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour
{
    //Transition
    [Space(10)] [Header("Transition")]
    [SerializeField] private GameObject _fader;
    private bool _isTimeToNight;

    //Reference
    [Space(10)] [Header("Reference")]
    [SerializeField] private float _timeMultiplier;
    [SerializeField] private Light _sunLight;
    [SerializeField] private float _startHour;

    //Lever-Coucher
    [Space(10)] [Header("Lever-Coucher")]
    [SerializeField] private float _sunriseHour;
    [SerializeField] public float _sunsetHour;

    //Affichage
    [Space(10)] [Header("Affichage")]
    [SerializeField] private TextMeshProUGUI textTime;

    [Space(10)] [Header("Time")]
    public DateTime _currentTime;
    TimeSpan _sunriseTime;
    TimeSpan _sunsetTime;
    public float LongTimeDay;

    [Space(10)] [Header("Environment")]
    [SerializeField] private GameObject Rain;
    [SerializeField] private TerrainSaver _terrainSaver;
    
    private void Start()
    {
        _currentTime = DateTime.Now.Date + TimeSpan.FromHours(_startHour);
        _sunriseTime = TimeSpan.FromHours(_sunriseHour);
        _sunsetTime = TimeSpan.FromHours(_sunsetHour);
        LongTimeDay = (((_sunsetHour - _sunriseHour) * 3600) / _timeMultiplier) / 60;

        if (GameData.IsRainning)
            Rain.SetActive(true);
    }

    private void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
        
        if (_currentTime >= DateTime.Now.Date + TimeSpan.FromHours(_sunsetHour) && _isTimeToNight == false)
        {
            _isTimeToNight = true;
            StartCoroutine(WaitingForSunSet());
        }
    }

    void UpdateTimeOfDay()
    {
        _currentTime = _currentTime.AddSeconds(Time.deltaTime * _timeMultiplier);
        textTime.text = _currentTime.ToString("HH:mm");
    }

    private void RotateSun()
    {
        float sunLightRotation;

        if (_currentTime.TimeOfDay > _sunriseTime && _currentTime.TimeOfDay < _sunsetTime)
        {
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(_sunriseTime, _sunsetTime);
            TimeSpan TimeSinceSunrise = CalculateTimeDifference(_sunriseTime, _currentTime.TimeOfDay);

            double percentage = TimeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(0, 180, (float) percentage);
        }
        else
        {
            TimeSpan sunsetToSunsetDuration = CalculateTimeDifference(_sunsetTime, _sunriseTime);
            TimeSpan TimeSinceSunset = CalculateTimeDifference(_sunsetTime, _currentTime.TimeOfDay);

            double percentage = TimeSinceSunset.TotalMinutes / sunsetToSunsetDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(180, 360, (float) percentage);
        }

        _sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
    }

    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;

        if (difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }

        return difference;
    }


    IEnumerator WaitingForSunSet()
    {
        _fader.SetActive(true);
        yield return new WaitForSeconds(3);
        _terrainSaver.OnApplicationQuit(); // reset terrain
        SceneManager.LoadScene("Night");
    }
}