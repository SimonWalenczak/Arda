using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayManager : MonoBehaviour
{
    [SerializeField] TerrainSaver _terrainSaver;
    public bool _isTuto;

    [Space(10)] [Header("Start")] public int StartHour;
    public int StartMinute;

    [Space(10)] [Header("End")] public int EndHour;
    public int EndMinute;

    [Space(10)] [Header("Control")] public int TimeMultiplier;
    [HideInInspector] public float Timer = 0.0f;

    [Space(10)] [Header("Debug")] public int TimeSinceStart;
    public int CurrentHour;
    public int CurrentMinute;
    public float CurrentSeconds = 0;
    private float _timeSinceStart;

    [Space(10)] [Header("UI")] public TextMeshProUGUI TimeOfDay;
    [SerializeField] private GameObject _fadeOut;

    int endSecs;

    [HideInInspector] public bool canActiveTimer;

    public static DayManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Timer = StartHour * 3600 + StartMinute * 60;
        endSecs = EndHour * 3600 + EndMinute * 60;

        CurrentHour = StartHour;
        CurrentMinute = StartMinute;

        //Détermine le numéro du jour
        GameData.NumberDays = _isTuto ? 1 : 2;

        if (_isTuto == false)
            canActiveTimer = true;
    }

    private void Update()
    {
        if(canActiveTimer)
            ActiveTimer();
    }

    private void ActiveTimer()
    {
        Timer += Time.deltaTime * TimeMultiplier;

        _timeSinceStart += Time.deltaTime;
        TimeSinceStart = (int)_timeSinceStart;

        CurrentSeconds += Time.deltaTime * TimeMultiplier;


        if (CurrentSeconds >= 60)
        {
            CurrentSeconds -= 60;
            CurrentMinute++;
            if (CurrentMinute >= 60)
            {
                CurrentMinute = 0;
                CurrentHour++;
            }
        }

        if (CurrentMinute < 10)
        {
            TimeOfDay.text = CurrentHour.ToString() + "h0" + CurrentMinute.ToString();
        }
        else
        {
            TimeOfDay.text = CurrentHour.ToString() + 'h' + CurrentMinute.ToString();
        }


        if (Timer >= endSecs)
        {
            //print("coucou");
            StartCoroutine(WaitingForSunSet());
        }
    }

    IEnumerator WaitingForSunSet()
    {
        _fadeOut.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        _terrainSaver.OnApplicationQuit(); // reset terrain
        SceneManager.LoadScene("Night");
    }
}