using System.Collections.Generic;
using UnityEngine;

public class Camp : MonoBehaviour
{
    public static bool Contains(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }

    [SerializeField] LayerMask TargetLayer;

    public PlayerController _arcadeCar;
    public int SoldierGenerateMax;
    public int SoldierInPlace;
    public int SelectedSoldier;
    public List<Soldier> _soldiers;

    public Camera cam;
    public GameObject SoldierCursor;

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

    public void CheckSoldierInPlace()
    {
        SoldierInPlace = 0;
        foreach (var soldier in _soldiers)
        {
            if (soldier.gameObject.activeInHierarchy)
                SoldierInPlace++;
        }
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
            }
            else
            {
                soldier.gameObject.SetActive(false);
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

                    if (_arcadeCar.CurrentCamp == this)
                    {
                        soldierCard = _arcadeCar.SoldierCardPanel.GetComponent<SoldierCard>();

                        soldierCard.LastNameText.SetText(soldier.LastName);
                        soldierCard.FirstNameText.SetText(soldier.FirstName);
                        soldierCard.AgeText.SetText(soldier.Age);
                        soldierCard.SituationText.SetText(soldier.Situation);
                        soldierCard.MilitaryRankText.SetText(soldier.MilitaryRank);

                        switch (soldier.InjuryType)
                        {
                            case 1:
                                soldierCard.InjurySprite.color = Color.green;
                                break;

                            case 2:
                                soldierCard.InjurySprite.color = Color.yellow;
                                break;

                            case 3:
                                soldierCard.InjurySprite.color = Color.red;
                                break;
                        }
                    }
                    else
                    {
                        soldierCard = null;
                    }
                }

                else if (_arcadeCar.CurrentCamp != null)
                {
                    _arcadeCar.CurrentCamp.SelectedSoldier++;
                }
            }
        }
    }

    private void UpdateSoldier()
    {
        foreach (var soldier in _soldiers)
        {
            if (soldier.InjuryTime <= _generateSoldier.InjuryTimer[2])
                soldier.InjuryType = 3;
            else if (soldier.InjuryTime <= _generateSoldier.InjuryTimer[1])
                soldier.InjuryType = 2;
            else
                soldier.InjuryType = 1;

            if (soldier.isOccuped)
            {
                soldier.InjuryTime -= Time.deltaTime;
            }
            else
            {
                soldier.InjuryTime = 150;
            }
            if (soldier.InjuryTime <= 0)
            {
                _arcadeCar.GetComponent<PlayerController>().soldiers.Remove(soldier);
                soldier.isOccuped = false;
                SoldierInPlace--;
            }
        }
    }

    public void StartHeal()
    {
        cam.gameObject.SetActive(true);
        _arcadeCar.Healing = true;
        _arcadeCar.CurrentSpeed = 0;
        _arcadeCar.CurrentTurnSpeed = 0;
        SelectedSoldier = 1;
    }

    public void HealSoldier()
    {
        if (Input.GetKeyDown(KeyCode.F) && _arcadeCar.CurrentCamp == this && _arcadeCar.Healing == true)
        {
            foreach (var soldier in _soldiers)
            {
                if (soldier.isSelected == true && soldier.isOccuped == true)
                {
                    _arcadeCar.GetComponent<PlayerController>().soldiers.Remove(soldier);
                    SoldierInPlace--;
                    soldier.Heal();
                    foreach (var soldierCard in _arcadeCar.soldiers)
                    {
                        soldierCard.InjuryTime -= _arcadeCar.HealTime[soldier.InjuryType - 1];
                    }
                }
            }
        }
    }
}