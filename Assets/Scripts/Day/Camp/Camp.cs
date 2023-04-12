using System.Collections.Generic;
using UnityEngine;

public class Camp : MonoBehaviour
{
    public PlayerController _playerController;
    public Camera cam;
    public List<SoldierStruct> _soldiers;
    public bool IsDiagnostised;
    [SerializeField] private int SelectedSoldier;

    [SerializeField] LayerMask TargetLayer;
    [SerializeField] SoldierStruct currentSoldier;
    [SerializeField] private GenerateSoldier _generateSoldier;
    
    public static bool Contains(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Contains(TargetLayer, other.gameObject.layer))
        {
            _playerController.CanHeal = true;
            _playerController.CurrentCamp = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Contains(TargetLayer, other.gameObject.layer))
        {
            _playerController.CanHeal = false;
            _playerController.CurrentCamp = null;
        }
    }

    public void StartHeal()
    {
        cam.gameObject.SetActive(true);
        _playerController.Diagnosing = true;
        _playerController.CurrentSpeed = 0;
        _playerController.CurrentTurnSpeed = 0;
    }

    private void Start()
    {
        _generateSoldier = GetComponent<GenerateSoldier>();
        SelectedSoldier = 1;
    }

    private void Update()
    {
        foreach (var soldier in _soldiers)
        {
            if (SelectedSoldier == soldier.Index)
                currentSoldier = soldier;
        }

        SoldierCardUpdate();
    }

    private void SoldierCardUpdate()
    {
        SoldierCard soldierCard = null;
        soldierCard = _playerController.SoldierCardPanel.GetComponent<SoldierCard>();

        soldierCard.NameText.SetText(currentSoldier.Name);
        soldierCard.AgeText.SetText(currentSoldier.Age);
        soldierCard.AchievementsText.SetText(currentSoldier.Achievements);
        soldierCard.MilitaryRankText.SetText(currentSoldier.MilitaryRank);
    }
}