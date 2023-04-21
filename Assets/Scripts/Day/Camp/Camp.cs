using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Camp : MonoBehaviour
{
    public PlayerManager PlayerManager;
    public Camera cam;
    public List<SoldierStruct> _soldiers;
    public bool IsDiagnostised;
    [SerializeField] private int SelectedSoldier;

    [SerializeField] LayerMask TargetLayer;
    [SerializeField] SoldierStruct currentSoldier;
    [SerializeField] private GenerateSoldier _generateSoldier;
    [SerializeField] private bool _isGenerate;

    [SerializeField] private GameObject _soldiersProps;
    [SerializeField] private GameObject _soldierSpriteParent;
    [SerializeField] private Animator _soldierSpriteParentAnimator;
    [SerializeField] private GameObject radio;
    public GameObject radioParent;

    [SerializeField] private List<BulletCreation> _bodyParts;
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
        if (Contains(TargetLayer, other.gameObject.layer))
        {
            PlayerManager.CanHeal = true;
            PlayerManager.CurrentCamp = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Contains(TargetLayer, other.gameObject.layer))
        {
            PlayerManager.CanHeal = false;
            PlayerManager.CurrentCamp = null;
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

    private void Start()
    {
        _generateSoldier = GetComponent<GenerateSoldier>();
        _soldiers = _generateSoldier.Soldiers;
        SelectedSoldier = 1;
        _soldierSpriteParentAnimator = _soldierSpriteParent.GetComponent<Animator>();
        UpdateSoldier();
    }

    private void Update()
    {
        UpdateSoldier();

        if (_isGenerate == false)
        {
            _isGenerate = true;
            InstantiateBullet();
        }

        SoldierCardUpdate();
    }

    private void UpdateSoldier()
    {
        foreach (var soldier in _soldiers)
        {
            if (SelectedSoldier == soldier.Index)
            {
                currentSoldier = soldier;
            }
        }
    }

    public void InstantiateBullet()
    {
        for (int i = 0; i < currentSoldier.NbBulletBust; i++)
            _bodyParts[0].CreateBullet();

        for (int i = 0; i < currentSoldier.NbBulletArmLeft; i++)
            _bodyParts[1].CreateBullet();

        for (int i = 0; i < currentSoldier.NbBulletArmRight; i++)
            _bodyParts[2].CreateBullet();

        for (int i = 0; i < currentSoldier.NbBulletLegLeft; i++)
            _bodyParts[3].CreateBullet();

        for (int i = 0; i < currentSoldier.NbBulletLegRight; i++)
            _bodyParts[4].CreateBullet();
    }

    public void NextSoldier()
    {
        foreach (var bullet in ActualBullets)
        {
            Destroy(bullet);
        }

        ActualBullets.Clear();

        if (SelectedSoldier < _soldiers.Count)
        {
            CheckSoldier();
            _soldierSpriteParentAnimator.Play(0);
            NbBulletFound = 0;
            SelectedSoldier++;
            UpdateSoldier();
            InstantiateBullet();
        }
        else if (SelectedSoldier == _soldiers.Count)
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