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
    private int _totalSoldierDead;
    private int _totalSoldierSaved;

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
                BilanPerte.SetActive(true);
                break;
            case 3:
                BilanPerte.SetActive(false);
                break;
            case 4:
                BilanSauve.SetActive(true);
                break;
            case 5:
                BilanSauve.SetActive(false);
                break;
            case 6:
                BGLettre.SetActive(true);
                LetterAppearing(RelativeLetter1.gameObject);
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