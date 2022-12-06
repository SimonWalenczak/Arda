using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _creditsPanel;
    [SerializeField] private GameObject _optionsPanel;
    [SerializeField] private GameObject _audioPanel;

    public void MissionButton()
    {
        SceneManager.LoadScene(1);
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
