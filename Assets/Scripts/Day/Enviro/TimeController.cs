using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour
{
    //Transition
    [SerializeField] private GameObject _fader;

    //Reference
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private float _timeMultiplier;
    [SerializeField] private Light _sunLight;
    [SerializeField] private float _startHour;

    //Lever-Coucher
    [SerializeField] private float _sunriseHour;
    [SerializeField] private float _sunsetHour;

    //Affichage
    [SerializeField] private TextMeshProUGUI textTime;

    private DateTime _currentTime;
    TimeSpan _sunriseTime;
    TimeSpan _sunsetTime;

    private void Start()
    {
        _currentTime = DateTime.Now.Date + TimeSpan.FromHours(_startHour);
        _sunriseTime = TimeSpan.FromHours(_sunriseHour);
        _sunsetTime = TimeSpan.FromHours(_sunsetHour);
        StartCoroutine(Waiting());
    }

    private void Update()
    {
        if (!_playerController.Healing)
        {
            UpdateTimeOfDay();
            RotateSun();
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


    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(((_sunsetHour - _sunriseHour) * 3600) / _timeMultiplier);
        _fader.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(2);
    }
}