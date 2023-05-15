using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Serialization;

public class GeneralDialog : MonoBehaviour
{
    public bool CanTalk;
    public bool CanUpgrade;

    [SerializeField] private TextMeshProUGUI _generalTextVisual;
    [SerializeField] private List<Dialog> _generalTextFirstNight;
    [SerializeField] private List<Dialog> _generalText;

    [SerializeField] private int index = 0;
    [SerializeField] private GameObject BGLettre;

    private void Start()
    {
        GameData.NumberDays = 1;

        CanTalk = false;
        StartCoroutine(WaitingForTalk());
    }

    private void Update()
    {
        if (CanTalk)
        {
            _generalTextVisual.gameObject.SetActive(true);

            if (GameData.NumberDays == 1)
            {
                if (Gamepad.current.buttonSouth.wasPressedThisFrame)
                {
                    if (index < _generalTextFirstNight.Count - 1)
                    {
                        if (index >= 5)
                        {
                            if (_generalTextFirstNight[index].HavePaper == false)
                            {
                                index++;
                            }
                            else
                            {
                                if (_generalTextFirstNight[index].PaperActif == false)
                                {
                                    _generalTextFirstNight[index].PaperActif = true;
                                    BGLettre.SetActive(true);
                                    LetterAppearing(_generalTextFirstNight[index].Paper);
                                }
                                else
                                {
                                    BGLettre.SetActive(false);
                                    LetterDisappearing(_generalTextFirstNight[index].Paper);
                                    index++;
                                }
                            }
                        }
                        else
                        {
                            if (index == 0)
                            {
                                if (GameData.HadSaveSoldier)
                                    index = 1;
                                else
                                    index = 3;
                            }
                            else
                            {
                                if (_generalTextFirstNight[index].PaperActif == false)
                                {
                                    _generalTextFirstNight[index].PaperActif = true;
                                    BGLettre.SetActive(true);
                                    LetterAppearing(_generalTextFirstNight[index].Paper);
                                }
                                else if (_generalTextFirstNight[index].PaperActif == true && (index == 2 || index == 4))
                                {
                                    BGLettre.SetActive(false);
                                    LetterDisappearing(_generalTextFirstNight[index].Paper);
                                    index = 5;
                                }
                                else if (_generalTextFirstNight[index].PaperActif == true)
                                {
                                    BGLettre.SetActive(false);
                                    LetterDisappearing(_generalTextFirstNight[index].Paper);
                                    index++;
                                }
                            }
                        }
                    }
                    else
                    {
                        CanUpgrade = true;
                    }
                }

                _generalTextVisual.SetText(_generalTextFirstNight[index].DialogText);
            }
            else
            {
                if (Gamepad.current.buttonSouth.wasPressedThisFrame)
                {
                    if (index < _generalText.Count - 1)
                    {
                        if (_generalText[index].HavePaper == false)
                        {
                            index++;
                        }
                        else
                        {
                            if (_generalText[index].PaperActif == false)
                            {
                                _generalText[index].PaperActif = true;
                                BGLettre.SetActive(true);
                                LetterAppearing(_generalText[index].Paper);
                            }
                            else
                            {
                                BGLettre.SetActive(false);
                                LetterDisappearing(_generalText[index].Paper);
                                index++;
                            }
                        }
                    }
                    else
                    {
                        CanUpgrade = true;
                    }
                }

                _generalTextVisual.SetText(_generalText[index].DialogText);
            }
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

    IEnumerator WaitingForTalk()
    {
        yield return new WaitForSeconds(3);
        CanTalk = true;
    }
}