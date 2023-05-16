using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _creditsPanel;
    [SerializeField] private GameObject _optionsPanel;
    [SerializeField] private GameObject _audioPanel;
    [SerializeField] private GameObject fadeOut;

    [Space(10)] [Header("Selection")] public int index;
    public GameObject actualCross;
    public List<GameObject> cross;

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
    }

    private void Update()
    {
        if (Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            switch (index)
            {
                case 0:
                    MissionButton();
                    break;

                case 1:
                    //OptionsButton();
                    break;

                case 2:
                    CreditsButton();
                    break;
            }
        }

        if (Gamepad.current.leftStick.down.wasPressedThisFrame)
        {
            if (index < 2)
            {
                index++;
            }
            else if (index == 2)
            {
                index = 0;
            }

            actualCross.GetComponent<Image>().DOFade(0f, 0f);
            actualCross = cross[index];
        }

        if (Gamepad.current.leftStick.up.wasPressedThisFrame)
        {
            if (index > 0)
            {
                index--;
            }
            else if (index == 0)
            {
                index = 2;
            }

            actualCross.GetComponent<Image>().DOFade(0f, 0f);
            actualCross = cross[index];
        }

        if (index > 2)
            index = 0;
        if (index < 0)
            index = 2;

        actualCross.GetComponent<Image>().DOFade(255f, 0f);
    }

    IEnumerator GoToDay()
    {
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Night");
    }

    IEnumerator GoToCredits()
    {
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Credits");
    }

    public void MissionButton()
    {
        StartCoroutine(GoToDay());
    }

    public void OptionsButton()
    {
        _optionsPanel.SetActive(true);
    }

    public void CreditsButton()
    {
        StartCoroutine(GoToCredits());
    }

    public void AudioButton()
    {
        _audioPanel.gameObject.SetActive(true);
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