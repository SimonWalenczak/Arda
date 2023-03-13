using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GeneralDialog : MonoBehaviour
{
    public bool CanTalk;
    public bool CanUpgrade;

    [SerializeField] private TextMeshProUGUI _generalTextVisual;
    [SerializeField] private List<Dialog> _generalText;
    private int index = 0;

    [SerializeField] private GameObject BilanPerte;
    [SerializeField] private GameObject BilanSauve;

    [SerializeField] private GameObject BGLettre;
    [SerializeField] private GameObject RelativeLetter1;
    [SerializeField] private GameObject RelativeLetter2;
    [SerializeField] private GameObject RequestLetter1;
    [SerializeField] private GameObject ThanksLetter1;

    
    private void Update()
    {
        if (CanTalk)
        {
            _generalTextVisual.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F) && index < _generalText.Count-1)
                index++;

            _generalTextVisual.SetText(_generalText[index].DialogText);
        }

        switch (index + 1)
        {
            case 0:
                BilanPerte.SetActive(false);
                BilanSauve.SetActive(false);
                break;
            case 2:
                BGLettre.SetActive(true);
                LetterAppearing(BilanPerte);
                break;
            case 3:
                BGLettre.SetActive(false);
                LetterDisappearing(BilanPerte);
                break;
            case 4:
                BGLettre.SetActive(true);
                LetterAppearing(BilanSauve);
                break;
            case 5:
                BGLettre.SetActive(false);
                LetterDisappearing(BilanSauve);
                break;
            case 6:
                BGLettre.SetActive(true);
                LetterAppearing(RelativeLetter1);
                break;
            case 7:
                LetterDisappearing(RelativeLetter1);
                LetterAppearing(RelativeLetter2);
                break;
            case 8:
                BGLettre.SetActive(false);
                LetterDisappearing(RelativeLetter2);
                break;
            case 9:
                BGLettre.SetActive(true);
                LetterAppearing(RequestLetter1);
                break;
            case 10:
                BGLettre.SetActive(false);
                LetterDisappearing(RequestLetter1);
                break;
            case 13:
                CanUpgrade = true;
                break;
        }
    }

    private void LetterAppearing(GameObject letter)
    {
        StartCoroutine(letter.GetComponent<Letter>().Appearing());
    }
    private void LetterDisappearing(GameObject letter)
    {
        StartCoroutine(letter.GetComponent<Letter>().Disappearing());

    }
}