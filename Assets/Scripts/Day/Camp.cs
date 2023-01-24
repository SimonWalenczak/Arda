using System;
using System.Collections.Generic;
using UnityEngine;

public class Camp : MonoBehaviour
{
    public static bool Contains(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }
    [SerializeField] LayerMask TargetLayer;

    [SerializeField] private ArcadeCar _arcadeCar;
    public GameObject HealingPanel;
    public int SelectedSoldier;

    public List<SoldierCard> _soldierCard;
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
        if (SelectedSoldier > 8)
            SelectedSoldier = 1;
        if (SelectedSoldier < 1)
            SelectedSoldier = 8;

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