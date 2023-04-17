using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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

    [SerializeField] private GameObject _soldiersProps;
    [SerializeField] private GameObject _soldierSpriteParent;
    [SerializeField] private Animator _soldierSpriteParentAnimator;
    [SerializeField] private GameObject radio;
    
    [SerializeField] private List<BulletCreation> _bodyParts;
    public List<GameObject> ActualBullets;
    [SerializeField] private int nbBulletFound;
    
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
        _soldiersProps.SetActive(false);
        radio.SetActive(true);
        
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

    private void Start()
    {
        _generateSoldier = GetComponent<GenerateSoldier>();
        _soldiers = _generateSoldier.Soldiers;
        SelectedSoldier = 1;
        _soldierSpriteParentAnimator = _soldierSpriteParent.GetComponent<Animator>();
    }

    private void Update()
    {
        foreach (var soldier in _soldiers)
        {
            if (SelectedSoldier == soldier.Index)
            {
                currentSoldier = soldier;
            }
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
            nbBulletFound = 0;
            SelectedSoldier++;
            UpdateSoldier();
            InstantiateBullet();
        }
        else if(SelectedSoldier == _soldiers.Count)
        {
            CheckSoldier();
            IsDiagnostised = true;
            cam.gameObject.SetActive(false);
            _playerController.Diagnosing = false;
            _playerController.ResetSpeed();
            _soldiersProps.SetActive(true);
            radio.SetActive(false);
            _playerController.SoldierCardPanel.SetActive(false);
        }
    }

    private void CheckSoldier()
    {
        currentSoldier.IsAlived = currentSoldier.TotalBullet == nbBulletFound ? true : false;

        if (currentSoldier.IsAlived)
            Debug.Log("saved");
        else
            Debug.Log(("dead"));
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