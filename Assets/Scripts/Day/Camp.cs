using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Camp : MonoBehaviour
{
    public static bool Contains(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }

    [SerializeField] LayerMask TargetLayer;

    [SerializeField] private ArcadeCar _arcadeCar;
    public int SoldierInPlace;
    public GameObject HealingPanel;
    public int SelectedSoldier;
    public List<SoldierCard> _soldierCard;


    [Space(10), Header("Generator Soldier")] [SerializeField]
    private float _timer = 5f;

    [SerializeField] private float _timerReset;
    [SerializeField] private GenerateSoldier _generateSoldier;


    private void Start()
    {
        _generateSoldier = GetComponent<GenerateSoldier>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Contains(TargetLayer, other.gameObject.layer))
        {
            _arcadeCar.CanHeal = true;
            _arcadeCar.CurrentCamp = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Contains(TargetLayer, other.gameObject.layer))
        {
            _arcadeCar.CanHeal = false;
            _arcadeCar.CurrentCamp = null;
        }
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _timer = _timerReset;
            _generateSoldier.GeneratorSoldier();
        }

        ActiveSoldierCard();
        CheckSelectSoldier();
        UpdateSoldier();
        HealSoldier();
    }

    private void ActiveSoldierCard()
    {
        if (SelectedSoldier > SoldierInPlace)
            SelectedSoldier = 1;
        if (SelectedSoldier < 1)
            SelectedSoldier = SoldierInPlace;

        for (int i = 0; i < SoldierInPlace; i++)
        {
            if (_soldierCard[i].isOccuped == true)
            {
                _soldierCard[i].gameObject.SetActive(true);
            }
            else
            {
                _soldierCard[i].gameObject.SetActive(false);
            }
        }
    }

    private void CheckSelectSoldier()
    {
        foreach (var soldierCard in _soldierCard)
        {
            if (soldierCard.index != SelectedSoldier)
            {
                soldierCard.isSelected = false;
            }
            else
            {
                if (soldierCard.isOccuped == true)
                    soldierCard.isSelected = true;
                else if (_arcadeCar.CurrentCamp != null)
                {
                    _arcadeCar.CurrentCamp.SelectedSoldier++;
                }
            }
        }
    }

    private void UpdateSoldier()
    {
        foreach (var soldier in _soldierCard)
        {
            if (soldier.InjuryTime <= _generateSoldier.InjuryTimer[2])
                soldier.InjuryType = 3;
            else if (soldier.InjuryTime <= _generateSoldier.InjuryTimer[1])
                soldier.InjuryType = 2;
            else
                soldier.InjuryType = 1;


            switch (soldier.InjuryType)
            {
                case 1:
                    soldier.InjurySprite.color = Color.green;
                    break;

                case 2:
                    soldier.InjurySprite.color = Color.yellow;
                    break;

                case 3:
                    soldier.InjurySprite.color = Color.red;
                    break;
            }

            soldier.InjuryTime -= Time.deltaTime;
            if (soldier.InjuryTime <= 0)
            {
                soldier.isOccuped = false;
            }
        }
    }

    public void OpenHealingPanel()
    {
        _arcadeCar.OnHealingMenu = true;
        HealingPanel.SetActive(true);
        _arcadeCar.CurrentSpeed = 0;
        _arcadeCar.CurrentTurnSpeed = 0;
        SelectedSoldier = 1;
    }

    public void HealSoldier()
    {
        if (Input.GetKeyDown(KeyCode.F) && _arcadeCar.CurrentCamp == this && _arcadeCar.OnHealingMenu == true)
        {
            foreach (var soldier in _soldierCard)
            {
                if (soldier.isSelected == true)
                {
                    soldier.Heal();
                    foreach (var soldierCard in _arcadeCar.SoldierCards)
                    {
                        soldierCard.InjuryTime -= _arcadeCar.HealTime[soldier.InjuryType - 1];
                    }
                }
            }
        }
    }
}