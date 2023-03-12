using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Camp : MonoBehaviour
{
    public static bool Contains(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }

    [SerializeField] LayerMask TargetLayer;
    
    [SerializeField] private ZoneManager _zoneManager;
    public PlayerController _playerController;
    public int SoldierGenerateMax;
    public int SoldierInPlace;
    public int SelectedSoldier;
    public List<Soldier> _soldiers;
    public int DeadChance;
    public Camera cam;
    public GameObject SoldierCursor;

    [Space(10), Header("Generator Soldier")] [SerializeField]
    private float _timer = 5f;

    [SerializeField] private float _timerReset;
    [SerializeField] private GenerateSoldier _generateSoldier;

    [SerializeField] private float _waitingForSpawn;


    private void Start()
    {
        _generateSoldier = GetComponent<GenerateSoldier>();
        _zoneManager = GetComponent<ZoneManager>();
        
        foreach (var soldier in _soldiers)
        {
            soldier.cam = cam;
        }
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

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _timer = _timerReset;
            if (_playerController.Healing == false && _generateSoldier.CanSpawn)
            {
                _generateSoldier.GeneratorSoldier();
            }
        }

        if (SoldierInPlace != 0)
        {
            SoldierCursor.SetActive(true);
        }
        else
        {
            SoldierCursor.SetActive(false);
        }

        ActiveSoldierCard();
        CheckSelectSoldier();
        UpdateSoldier();
        HealSoldier();
    }

    public void WaitingForSpawn()
    {
        _generateSoldier.CanSpawn = false;
        StartCoroutine(WaintingSpawn());
    }

    IEnumerator WaintingSpawn()
    {
        yield return new WaitForSeconds(_waitingForSpawn);
        _generateSoldier.CanSpawn = true;
    }

    private void ActiveSoldierCard()
    {
        if (SelectedSoldier > SoldierGenerateMax)
            SelectedSoldier = 1;
        if (SelectedSoldier < 1)
            SelectedSoldier = SoldierInPlace;

        foreach (var soldier in _soldiers)
        {
            if (soldier.isOccuped == true)
            {
                soldier.gameObject.SetActive(true);
                if (_playerController.Healing && _playerController.CurrentCamp == this)
                    soldier.LifeBarParent.SetActive(true);
                else
                    soldier.LifeBarParent.SetActive(false);
            }
            else
            {
                soldier.gameObject.SetActive(false);
                soldier.LifeBarParent.SetActive(false);
            }
        }
    }

    private void CheckSelectSoldier()
    {
        SoldierCard soldierCard = null;

        foreach (var soldier in _soldiers)
        {
            if (soldier.index != SelectedSoldier)
            {
                soldier.isSelected = false;
            }
            else
            {
                if (soldier.isOccuped == true)
                {
                    soldier.isSelected = true;
                    SoldierCursor.transform.position = new Vector3(soldier.transform.position.x,
                        soldier.transform.position.y + 1.5f, soldier.transform.position.z);
                    SoldierCursor.transform.LookAt(cam.transform);

                    if (_playerController.CurrentCamp == this)
                    {
                        //Set SoldierCard
                        soldierCard = _playerController.SoldierCardPanel.GetComponent<SoldierCard>();
                        switch (soldier.InjuryTypeOrigin)
                        {
                            case 1:
                                soldierCard.InjurySprite.color = Color.green;
                                soldierCard.InjuryText.SetText("1");
                                break;

                            case 2:
                                soldierCard.InjurySprite.color = Color.yellow;
                                soldierCard.InjuryText.SetText("2");
                                break;

                            case 3:
                                soldierCard.InjurySprite.color = Color.red;
                                soldierCard.InjuryText.SetText("4");
                                break;
                        }

                        if (soldier.isDiagnosed)
                        {
                            soldierCard.DiagnosticParent.SetActive(false);
                        }
                        else
                        {
                            soldierCard.DiagnosticParent.SetActive(true);
                        }

                        soldierCard.LastNameText.SetText(soldier.LastName);
                        soldierCard.FirstNameText.SetText(soldier.FirstName);
                        soldierCard.AgeText.SetText(soldier.Age);
                        soldierCard.SituationText.SetText(soldier.Situation);
                        soldierCard.MilitaryRankText.SetText(soldier.MilitaryRank);
                        
                        soldierCard.BodyHairColor = soldier.BodyHairColor;
                        soldierCard.FaceSprite.sprite = soldier.Face;
                        soldierCard.BodyHairSprite.sprite = soldier.BodyHair;
                        soldierCard.BodyHairSprite.color = soldier.BodyHairColor;
                    }
                    else
                    {
                        soldierCard = null;
                    }
                }

                else if (_playerController.CurrentCamp != null)
                {
                    _playerController.CurrentCamp.SelectedSoldier++;
                }
            }
        }
    }

    private void UpdateSoldier()
    {
        foreach (var soldier in _soldiers)
        {
            //Update injury type
            if (soldier.InjuryTime <= _generateSoldier.InjuryTimer[2])
                soldier.InjuryType = 3;
            else if (soldier.InjuryTime <= _generateSoldier.InjuryTimer[1])
                soldier.InjuryType = 2;
            else
                soldier.InjuryType = 1;

            //Set Injury Time
            soldier.UnitToSec = _generateSoldier.InjuryTimerMax / 10;
            //Change color of lifebar
            switch (soldier.InjuryType)
            {
                case 1:
                    soldier.LifeBar.GetComponent<Image>().color = Color.green;
                    break;

                case 2:
                    soldier.LifeBar.GetComponent<Image>().color = Color.yellow;
                    break;

                case 3:
                    soldier.LifeBar.GetComponent<Image>().color = Color.red;
                    break;
            }

            if (soldier.isOccuped)
            {
                if (soldier.InjuryTime <= (soldier.LifeTimeStep - (_generateSoldier.InjuryTimer[0] / 10)))
                {
                    soldier.LifeBar.fillAmount -= 0.1f;
                    soldier.LifeTimeStep -= _generateSoldier.InjuryTimer[0] / 10;
                }
            }
            else
            {
                soldier.InjuryTime = 1000;
                soldier.LifeBar.fillAmount = 1;
            }

            if (soldier.InjuryTime <= 0)
            {
                _playerController.GetComponent<PlayerController>().soldiers.Remove(soldier);
                soldier.isDiagnosed = false;
                soldier.isOccuped = false;
                SoldierInPlace--;
                _zoneManager.TotalDead++;
                switch (soldier.MilitaryRank)
                {
                    case "Soldat":
                        _zoneManager.TotalFirstClassDead++;
                        break;
                    
                    case "Elite":
                        _zoneManager.TotalElitDead++;
                        break;
                    
                    case "Officier":
                        _zoneManager.TotalOfficierSaved++;
                        break;
                }
            }
        }
    }

    public void StartHeal()
    {
        cam.gameObject.SetActive(true);
        _playerController.Healing = true;
        _playerController.CurrentSpeed = 0;
        _playerController.CurrentTurnSpeed = 0;
        foreach (var soldier in _soldiers)
        {
            if (soldier.isOccuped && soldier.isSelected)
                SelectedSoldier = soldier.index;
            break;
        }
    }

    public void HealSoldier()
    {
        if (Input.GetKeyDown(KeyCode.F) && _playerController.CurrentCamp == this && _playerController.Healing == true)
        {
            foreach (var soldier in _soldiers)
            {
                if (soldier.isSelected == true && soldier.isOccuped == true)
                {
                    if (soldier.isDiagnosed)
                    {
                        _playerController.GetComponent<PlayerController>().soldiers.Remove(soldier);
                        SoldierInPlace--;
                        
                        
                        if (soldier.InjuryType < 3)
                        {
                            print($"Soldier {soldier.LastName} {soldier.FirstName} is safe.");
                            switch (soldier.MilitaryRank)
                            {
                                case "Soldat":
                                    _zoneManager.TotalFirstClassSaved++;
                                    break;
                    
                                case "Elite":
                                    _zoneManager.TotalElitSaved++;
                                    break;
                    
                                case "Officier":
                                    _zoneManager.TotalOfficierSaved++;
                                    break;
                            }
                        }
                        else
                        {
                            int _deadChance = Random.Range(0, 101);
                            if (_deadChance <= DeadChance)
                            {
                                print($"Soldier {soldier.LastName} {soldier.FirstName} is dead.");
                                switch (soldier.MilitaryRank)
                                {
                                    case "Soldat":
                                        _zoneManager.TotalFirstClassDead++;
                                        break;
                    
                                    case "Elite":
                                        _zoneManager.TotalElitDead++;
                                        break;
                    
                                    case "Officier":
                                        _zoneManager.TotalOfficierDead++;
                                        break;
                                }
                            }
                            else
                            {
                                print($"Soldier {soldier.LastName} {soldier.FirstName} is safe.");
                                switch (soldier.MilitaryRank)
                                {
                                    case "Soldat":
                                        _zoneManager.TotalFirstClassSaved++;
                                        break;
                    
                                    case "Elite":
                                        _zoneManager.TotalElitSaved++;
                                        break;
                    
                                    case "Officier":
                                        _zoneManager.TotalOfficierSaved++;
                                        break;
                                }
                            }
                        }
                        soldier.isDiagnosed = false;
                        soldier.isOccuped = false;
                        
                        
                        foreach (var soldierCard in _playerController.soldiers)
                        {
                            soldierCard.InjuryTime -=
                                soldier.UnitToSec * _playerController.HealTime[soldier.InjuryTypeOrigin - 1];
                        }
                    }
                    else
                    {
                        soldier.isDiagnosed = true;
                        foreach (var soldierCard in _playerController.soldiers)
                        {
                            soldierCard.InjuryTime -= soldier.UnitToSec;
                        }
                    }
                }
            }
        }
    }

    public void CheckHeal()
    {
        
    }
}