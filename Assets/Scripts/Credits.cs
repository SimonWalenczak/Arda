using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Credits : MonoBehaviour
{
    [SerializeField] private GameObject FadeOut;

    private bool _canGoToMainMenu;

    public int SecondBeforeCredits;

    private void Start()
    {
        StartCoroutine(WaitingForCredits());
    }

    private void Update()
    {
        if (Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            if (_canGoToMainMenu)
                StartCoroutine(GoToMainMenu());
        }
    }

    IEnumerator WaitingForCredits()
    {
        yield return new WaitForSeconds(SecondBeforeCredits);
        _canGoToMainMenu = true;
    }

    IEnumerator GoToMainMenu()
    {
        FadeOut.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainMenu");
    }
}