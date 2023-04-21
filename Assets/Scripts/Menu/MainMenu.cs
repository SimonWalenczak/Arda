using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Unity.VisualScripting;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _creditsPanel;
    [SerializeField] private GameObject _optionsPanel;
    [SerializeField] private GameObject _audioPanel;
    [SerializeField] private GameObject fadeOut;
    
    private void Start()
    {
        GameData.NumberDays = 1;
        GameData.IsRainning = false;
        GameData.IsSunning = false;
        GameData.SoftFight = false;
        GameData.HardFight = false;
        GameData.BombingNb = 0;
        GameData.UnderminedInfiltrationNb = 0;
        GameData.InfantryChargeNb = 0;
        
        print(GameData.IsRainning);
    }

    IEnumerator FadeOut()
    {
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }
    public void MissionButton()
    {
        StartCoroutine(FadeOut());
    }
    public void OptionsButton()
    {
        _optionsPanel.SetActive(true);
    }
    public void CreditsButton()
    {
        _creditsPanel.gameObject.SetActive(true);
    }
    public void AudioButton()
    {
        _audioPanel.gameObject.SetActive(true);
    }

    public void CloseCredits()
    {
        _creditsPanel.gameObject.SetActive(false);
    }

    public void BackToOptions()
    {
        _audioPanel.SetActive(false);
    }
    
    public void BackToGame()
    {
        _optionsPanel.SetActive(false);
    }
}
