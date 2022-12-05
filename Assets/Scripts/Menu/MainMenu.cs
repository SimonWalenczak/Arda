using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Image _missionButton;
    [SerializeField] private Image _optionsButton;
    [SerializeField] private Image _creditsButton;
    [SerializeField] private GameObject _creditsPanel;

    [SerializeField] Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    public void ButtonAppear()
    {
        var imageColor = _image.color;
        imageColor.a = 0f;
    }

    public void ButtonDisappear()
    {
        var alpha = 255f;
        _image.color.a = alpha;
    }

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
