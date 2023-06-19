using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GeneralDialog : MonoBehaviour
{
    [SerializeField] private bool _debugDay;
    public int DayNumber;
    public bool CanTalk;
    public bool CanUpgrade;

    [SerializeField] private TextMeshProUGUI _generalTextVisual;
    [SerializeField] private TextMeshProUGUI _bodyTextVisual;
    [SerializeField] private List<Dialog> _generalTextFirstNight;
    [SerializeField] private List<Dialog> _generalText;
    [SerializeField] private GameObject _generalSprite;
    [SerializeField] private GameObject _soldatSpriteFinal;
    [SerializeField] private Image colorFinalSoldat;

    [SerializeField] private int index = 0;
    [SerializeField] private GameObject BGLettre;

    private int _bilanIndex = 0;
    public GameObject BilanCursor;
    public List<RectTransform> CursorOrigin;
    public List<GameObject> RewardsGaugesText;
    [SerializeField] private TextMeshProUGUI _letterTextVisual;

    [Space(10)] [Header("Lettre Envoie Nuit 1")] [SerializeField]
    private List<String> _letterText;

    [SerializeField] private List<LetterRewards> _letterRewardsList;

    [SerializeField] private int _letterIndex = 0;
    [SerializeField] private List<GameObject> BodyFaces;
    [SerializeField] private List<GameObject> SentLetters;
    private GameObject _actualFace;

    [Space(10)] [Header("Choix Final Nuit 2")] [SerializeField]
    private List<String> _choixText;

    [SerializeField] private int _choixFinalIndex = 0;
    [SerializeField] private List<GameObject> BodyFacesFinalChoice;

    private void Start()
    {
        if (_debugDay)
            GameData.NumberDays = DayNumber;

        CanTalk = false;
        StartCoroutine(WaitingForTalk());

        if (GameData.NumberDays == 1)
            _actualFace = BodyFaces[_letterIndex];
        else if (GameData.NumberDays == 2)
        {
            _generalSprite.SetActive(false);
            _actualFace = BodyFacesFinalChoice[_choixFinalIndex];
            colorFinalSoldat = _soldatSpriteFinal.GetComponent<Image>();
            colorFinalSoldat.color = Color.black;
            Debug.Log(colorFinalSoldat);
        }

        ExtraDialogue();
    }

    private void DialogGeneral()
    {
        _generalTextVisual.gameObject.SetActive(true);

        if (GameData.NumberDays == 1)
        {
            if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                if (index < _generalTextFirstNight.Count - 1)
                {
                    if (index == 8)
                    {
                        foreach (var sprite in BodyFaces)
                        {
                            sprite.SetActive(true);
                        }
                    }

                    if (index == 0 || (index >= 6 && index != 9))
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
                    else if (index == 9)
                    {
                        if (_generalTextFirstNight[index].PaperActif == false)
                        {
                            _generalTextFirstNight[index].PaperActif = true;
                            foreach (var sprite in BodyFaces)
                            {
                                sprite.SetActive(false);
                            }

                            LetterWrite(SentLetters[_letterIndex]);
                        }
                        else
                        {
                            BGLettre.SetActive(false);
                            _generalTextVisual.gameObject.SetActive(true);
                            _letterTextVisual.gameObject.SetActive(false);
                            LetterSend(SentLetters[_letterIndex]);
                            index++;
                        }
                    }
                    else
                    {
                        if (index == 1)
                        {
                            if (GameData.HasSavesSoldier)
                                index = 2;
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
                            else if (_generalTextFirstNight[index].PaperActif == true && (index == 2 || index == 3))
                            {
                                BGLettre.SetActive(false);
                                LetterDisappearing(_generalTextFirstNight[index].Paper);
                                index = 4;
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

            _bodyTextVisual.SetText(_generalTextFirstNight[index].BodyDialog);
            _generalTextVisual.SetText(_generalTextFirstNight[index].DialogText);
        }
        else
        {
            if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                if (index < _generalText.Count - 1)
                {
                    if (index == 2)
                    {
                        if (_generalText[index].PaperActif == true)
                        {
                            _soldatSpriteFinal.SetActive(true);
                        }
                    }

                    if (index == 3)
                    {
                        colorFinalSoldat.color = Color.white;
                    }

                    if (index == 7)
                    {
                        foreach (var sprite in BodyFacesFinalChoice)
                        {
                            sprite.SetActive(true);
                        }
                    }

                    if (index == 8)
                    {
                        BGLettre.SetActive(false);

                        _generalTextVisual.gameObject.SetActive(true);
                        _letterTextVisual.gameObject.SetActive(false);
                        index++;
                    }
                    else if (_generalText[index].HavePaper == false)
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
                    StartCoroutine(GoToFin());
                }
            }

            _bodyTextVisual.SetText(_generalText[index].BodyDialog);
            _generalTextVisual.SetText(_generalText[index].DialogText);
        }
    }

    private IEnumerator GoToFin()
    {
        if (_choixFinalIndex == 0)
            GameData.SaveGeorges = true;
        else
            GameData.SaveGeorges = false;

        gameObject.GetComponent<NightManager>().FadeOutCredits.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("FinHistoire");
    }

    private void DialogChoice()
    {
        BGLettre.SetActive(true);
        _generalTextVisual.gameObject.SetActive(false);
        _letterTextVisual.gameObject.SetActive(true);

        if (Gamepad.current.leftStick.right.wasPressedThisFrame)
        {
            if (_letterIndex < 2)
            {
                _letterIndex++;
            }
            else if (_letterIndex == 2)
            {
                _letterIndex = 0;
            }

            _actualFace.GetComponent<RectTransform>().DOScale(1f, 0.7f);
            _actualFace = BodyFaces[_letterIndex];
        }

        if (Gamepad.current.leftStick.left.wasPressedThisFrame)
        {
            if (_letterIndex > 0)
            {
                _letterIndex--;
            }
            else if (_letterIndex == 0)
            {
                _letterIndex = 2;
            }

            _actualFace.GetComponent<RectTransform>().DOScale(1f, 0.7f);
            _actualFace = BodyFaces[_letterIndex];
        }

        if (_letterIndex > 2)
            _letterIndex = 0;
        if (_letterIndex < 0)
            _letterIndex = 2;

        _actualFace.GetComponent<RectTransform>().DOScale(1.2f, 0.7f);
        _letterTextVisual.SetText(
            $"{_letterText[_letterIndex]}\n \nVous gagnerez {_letterRewardsList[_letterIndex].rewardText}");
    }

    private void DialogFinalChoice()
    {
        BGLettre.SetActive(true);
        _generalTextVisual.gameObject.SetActive(false);
        _letterTextVisual.gameObject.SetActive(true);

        if (Gamepad.current.leftStick.right.wasPressedThisFrame)
        {
            if (_choixFinalIndex == 0)
            {
                _choixFinalIndex = 1;
            }
            else if (_choixFinalIndex == 1)
            {
                _choixFinalIndex = 0;
            }

            _actualFace.GetComponent<RectTransform>().DOScale(1f, 0.7f);
            _actualFace = BodyFacesFinalChoice[_choixFinalIndex];
        }

        if (Gamepad.current.leftStick.left.wasPressedThisFrame)
        {
            if (_choixFinalIndex == 0)
            {
                _choixFinalIndex = 1;
            }
            else if (_choixFinalIndex == 1)
            {
                _choixFinalIndex = 0;
            }

            _actualFace.GetComponent<RectTransform>().DOScale(1f, 0.7f);
            _actualFace = BodyFacesFinalChoice[_choixFinalIndex];
        }

        if (_choixFinalIndex < 0)
            _choixFinalIndex = 1;
        if (_choixFinalIndex > 1)
            _choixFinalIndex = 0;

        _actualFace.GetComponent<RectTransform>().DOScale(1.2f, 0.7f);
        _letterTextVisual.SetText(_choixText[_choixFinalIndex]);
    }

    private void Update()
    {
        if (CanTalk)
            DialogGeneral();

        if (GameData.NumberDays == 1 && index == 9)
            DialogChoice();

        if (GameData.NumberDays == 2 && index == 8)
            DialogFinalChoice();

        if ((GameData.NumberDays == 1 && (index == 2 || index == 4)) || (GameData.NumberDays == 2 && index == 0))
            BilanPhase();
    }

    private void BilanPhase()
    {
        if (GameData.NumberDays == 2)
        {
            BilanCursor.SetActive(true);
            RewardsGaugesText[_bilanIndex].SetActive(true);
        }

        if (Gamepad.current.leftStick.down.wasPressedThisFrame)
        {
            _bilanIndex++;
            if (_bilanIndex > 2)
                _bilanIndex = 0;
            if (_bilanIndex < 0)
                _bilanIndex = 2;
            if (GameData.NumberDays == 2)
            {
                foreach (var rewardGauge in RewardsGaugesText)
                {
                    rewardGauge.SetActive(false);
                }

                RewardsGaugesText[_bilanIndex].SetActive(true);
            }
        }

        if (Gamepad.current.leftStick.up.wasPressedThisFrame)
        {
            _bilanIndex--;
            if (_bilanIndex > 2)
                _bilanIndex = 0;
            if (_bilanIndex < 0)
                _bilanIndex = 2;

            if (GameData.NumberDays == 2)
            {
                foreach (var rewardGauge in RewardsGaugesText)
                {
                    rewardGauge.SetActive(false);
                }

                RewardsGaugesText[_bilanIndex].SetActive(true);
            }
        }

        BilanCursor.GetComponent<RectTransform>().position =
            CursorOrigin[_bilanIndex].GetComponent<RectTransform>().position;


        if (Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            BilanCursor.SetActive(false);
        }
    }

    private void ExtraDialogue()
    {
        //Météo
        _generalTextFirstNight[10].DialogText = "Maintenant, les prévisions météorologiques.\n";
        if (GameData.IsRainning)
        {
            _generalTextFirstNight[10].DialogText +=
                "Demain sera une journée pluvieuse, attention au risque de glissage.";
            if (GameData.HasFog)
                _generalTextFirstNight[10].DialogText +=
                    " Et vous aurez également le droit à un épais brouillard sur les monts, alors prenez garde.";
            else
                _generalTextFirstNight[10].DialogText += " Mais vous échapperez tout de même au brouillard de plaine.";
        }
        else
        {
            _generalTextFirstNight[10].DialogText += "Demain sera une journée ensoleillée, aucun risque de glissage.";
            if (GameData.HasFog)
                _generalTextFirstNight[10].DialogText +=
                    " Néanmoins vous allez avoir le droit à du brouillard, alors ne relachez pas votre vigilance jeune fille.";
            else
                _generalTextFirstNight[10].DialogText += "";
        }

        //Combat
        _generalTextFirstNight[11].DialogText = "Pour ce qui est du combat, voici ce que l'on sait : \n";
        if (GameData.SoftFight && GameData.HardFight == false)
        {
            _generalTextFirstNight[11].DialogText +=
                "Les milices ennemies ne prévoient pas d'assauts, tout comme nous, donc demain sera une journée avec de faibles combats.";
        }
        else if (GameData.HardFight && GameData.SoftFight == false)
        {
            _generalTextFirstNight[11].DialogText +=
                "Les milices ennemies prévoient de forts assauts, tout comme nous, demain sera donc une journée rouge pour nos ennemis. " +
                "Et je ne l'espère pas pour nous...";
        }
        else if (GameData.SoftFight == false && GameData.HardFight == false)
        {
            _generalTextFirstNight[11].DialogText +=
                "Les milices ennemies comptent nous attaquer avec la même intensité qu'aujourd'hui, donc tenez-vous prête.";
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

    private void LetterWrite(GameObject letter)
    {
        StartCoroutine(letter.GetComponent<Letter>().WriteLetter());
    }

    private void LetterSend(GameObject letter)
    {
        StartCoroutine(letter.GetComponent<Letter>().SendLetter());
    }

    IEnumerator WaitingForTalk()
    {
        yield return new WaitForSeconds(3);
        CanTalk = true;
    }
}