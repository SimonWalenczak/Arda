using System.Collections.Generic;
using UnityEngine;

public class Camp : MonoBehaviour
{
    public PlayerController _playerController;
    public Camera cam;
    public List<Soldier> _soldiers;
    public bool IsDiagnostised;
    [SerializeField] private int SelectedSoldier;

    [SerializeField] LayerMask TargetLayer;
    [SerializeField] Soldier currentSoldier;

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

        soldierCard.LastNameText.SetText(currentSoldier.LastName);
        soldierCard.FirstNameText.SetText(currentSoldier.FirstName);
        soldierCard.AgeText.SetText(currentSoldier.Age);
        soldierCard.SituationText.SetText(currentSoldier.Situation);
        soldierCard.MilitaryRankText.SetText(currentSoldier.MilitaryRank);

        soldierCard.BodyHairColor = currentSoldier.BodyHairColor;
        soldierCard.FaceSprite.sprite = currentSoldier.Face;
        soldierCard.BodyHairSprite.sprite = currentSoldier.BodyHair;
        soldierCard.BodyHairSprite.color = currentSoldier.BodyHairColor;
    }
}