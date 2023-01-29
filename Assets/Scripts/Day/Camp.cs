using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

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


    [Header("\n------Generator Soldier------\n")] 
    [SerializeField] private float _timer = 5f;
    [SerializeField] private float _timerReset;
    [SerializeField] private GenerateSoldier _generateSoldier;

    private void Start()
    {
        _generateSoldier = GetComponent<GenerateSoldier>();
        SelectedSoldier = 1;
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
    }
    
    private void ActiveSoldierCard()
    {
        if (SelectedSoldier > SoldierInPlace)
            SelectedSoldier = 1;
        if (SelectedSoldier < 1)
            SelectedSoldier = SoldierInPlace;

        for (int i = 0; i < SoldierInPlace; i++)
        {
            _soldierCard[i].gameObject.SetActive(true);
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
                soldierCard.isSelected = true;
            }
        }
    }

    public void StartHealing()
    {
        _arcadeCar.OnHealingMenu = true;
        HealingPanel.SetActive(true);
        _arcadeCar.CurrentSpeed = 0;
        _arcadeCar.CurrentTurnSpeed = 0;
        SelectedSoldier = 1;
    }
}