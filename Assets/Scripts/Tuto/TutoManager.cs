using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutoManager : MonoBehaviour
{
    public static TutoManager Instance;
    public ArcadeCar arcadeCar;
    public ChoixDirection choixDirection;

    [Header("Tuto Variables")] public int IndexTuto = 0;
    public bool IsTextTuto;

    [Space(10)] [Header("Tuto Reference")] public List<String> TextTuto;
    public GameObject TutoParent;
    public TMP_Text TextTutoText;
    public GameObject ButtonDisplayParent;
    public GameObject ButtonBDisplay;
    public GameObject TutoCarPanel;
    public GameObject AlertePanel;
    public GameObject SelectButton;

    public bool endFirstTent;
    public bool endSecondTent;
    public bool CanMakeChoice;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        IsTextTuto = true;
    }

    private void Update()
    {
        if (IsTextTuto)
        {
            if (IndexTuto < 14)
            {
                if (IndexTuto <= 4 && endFirstTent == false)
                {
                    TutoParent.SetActive(true);
                    TextTutoText.SetText(TextTuto[IndexTuto]);
                }
                else if (IndexTuto > 4 && IndexTuto <= 10 && endSecondTent == false)
                {
                    TutoParent.SetActive(true);
                    TextTutoText.SetText(TextTuto[IndexTuto]);
                }

                if (Gamepad.current.buttonWest.wasPressedThisFrame)
                {
                    if (IndexTuto == 7)
                        IndexTuto = 8;
                }

                if (Gamepad.current.buttonSouth.wasPressedThisFrame)
                {
                    if (IndexTuto == 4 || IndexTuto == 10)
                    {
                        RadiologyPhase.Instance.LeaveTent();
                        if (IndexTuto == 10)
                        {
                            IndexTuto++;
                            IsTextTuto = false;
                        }
                    }
                    else if (IndexTuto == 7)
                    {
                        return;
                    }
                    else
                    {
                        if (IndexTuto != 6)
                            IndexTuto++;
                        else
                        {
                            TextTutoText.SetText(TextTuto[7]);
                        }
                    }
                }
            }
        }

        if (IndexTuto == 5)
        {
            ButtonBDisplay.SetActive(true);
            ButtonDisplayParent.SetActive(true);
        }

        if (IndexTuto == 0)
            arcadeCar.controllable = false;

        if (IndexTuto == 2)
            ButtonDisplayParent.SetActive(true);
    }
}