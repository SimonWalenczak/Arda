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

    public void MissionButton()
    {
        SceneManager.LoadScene(1);
    }
    public void OptionsButton()
    {
        
    }
    public void CreditsButton()
    {
        _creditsPanel.gameObject.SetActive(true);
    }

    public void CloseCredits()
    {
        _creditsPanel.gameObject.SetActive(false);
    }
}
