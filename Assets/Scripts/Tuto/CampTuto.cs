using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampTuto : MonoBehaviour
{
    public PlayerManager PlayerManager;
    public Camera cam;
    public List<SoldierStruct> _soldiers;
    [SerializeField] private int SelectedSoldier;
    [SerializeField] SoldierStruct currentSoldier;
    public bool IsDiagnostised;
    public LayerMask PlayerLayer;

    [SerializeField] private GameObject _soldiersProps;
    [SerializeField] private GameObject _soldierSpriteParent;
    [SerializeField] private Animator _soldierSpriteParentAnimator;
    [SerializeField] private GameObject radio;
    public GameObject radioParent;

    public List<GameObject> ActualBullets;
    public int NbBulletFound;

    public int TotalSaved = 0;
    public int TotalDead = 0;

    public static bool Contains(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Contains(PlayerLayer, other.gameObject.layer))
        {
            PlayerManager.CanHeal = true;
            //PlayerManager.CampTuto = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Contains(PlayerLayer, other.gameObject.layer))
        {
            PlayerManager.CanHeal = false;
            //PlayerManager.CampTuto = null;
        }
    }

    public void StartHeal()
    {
        cam.gameObject.SetActive(true);
        PlayerManager.Diagnosing = true;

        // Speed 0
        PlayerManager.GetComponent<ArcadeCar>().enabled = false;
        //

        _soldiersProps.SetActive(false);
        radio.SetActive(true);
        PlayerManager._radio = radio.GetComponent<Radio>();
        radioParent.SetActive(true);

        for (int i = 0; i < TotalSaved; i++)
            PlayerManager.SoldierSaved[i].SetActive(false);
        for (int i = 0; i < TotalDead; i++)
            PlayerManager.SoldierDead[i].SetActive(false);
    }

    public void NextSoldier()
    {
        if (SelectedSoldier == _soldiers.Count)
        {
            CheckSoldier();
            IsDiagnostised = true;
            cam.gameObject.SetActive(false);
            PlayerManager.Diagnosing = false;
            PlayerManager.ResetSpeed();
            _soldiersProps.SetActive(true);
            radio.SetActive(false);
            PlayerManager.SoldierCardPanel.SetActive(false);
            PlayerManager.FicheBilan.SetActive(true);
            PlayerManager.OnBilan = true;
            PlayerManager._radio = null;
            radioParent.SetActive(false);

            for (int i = 0; i < TotalSaved; i++)
                PlayerManager.SoldierSaved[i].SetActive(true);
            for (int i = 0; i < TotalDead; i++)
                PlayerManager.SoldierDead[i].SetActive(true);
        }
    }

    private void CheckSoldier()
    {
        currentSoldier.IsAlived = currentSoldier.TotalBullet == NbBulletFound ? true : false;

        if (currentSoldier.IsAlived)
            TotalSaved++;
        else
            TotalDead++;
    }

    private void SoldierCardUpdate()
    {
        SoldierCard soldierCard = null;
        soldierCard = PlayerManager.SoldierCardPanel.GetComponent<SoldierCard>();

        soldierCard.NameText.SetText(currentSoldier.Name);
        soldierCard.AgeText.SetText(currentSoldier.Age);
        soldierCard.AchievementsText.SetText(currentSoldier.Achievements);
        soldierCard.MilitaryRankText.SetText(currentSoldier.MilitaryRank);
    }
}
