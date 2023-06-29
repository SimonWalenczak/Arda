using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DataCenterDay : MonoBehaviour
{
    public static DataCenterDay Instance;

    [HideInInspector] public List<SoldierInfo> CurrentSoldiers = new List<SoldierInfo>();
    [HideInInspector] public List<GameObject> CurrentBullets = new List<GameObject>();
    public List<SoldierCard> CurrentInfoSoldiers = new List<SoldierCard>();
    [HideInInspector] public Tent CurrentTent;
    [HideInInspector] public int BulletsFound = 0;
    [HideInInspector] public bool isABulletFound;

    [Header("Map")] [SerializeField] private GameObject _map;
    public GameObject MarqueurSelection;
    public GameObject MarqueurTent;
    public bool Marked;
    public int indexTent = 0;
    public bool CanOpenMap;
    public List<RectTransform> tents;
    public List<RectTransform> tentsTuto;

    [Header("Color Soldiers")] public Color colorGreen;
    public Color colorOrange;
    public Color colorRed;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (GameData.NumberDays == 2)
        {
            CanOpenMap = true;
            ResetValuesAfterTuto();
        }
        else
        {
        }
    }

    private void ResetValuesAfterTuto()
    {
        GlobalManager.Instance.GaugesValues[0].ActualValue = 0;
        GlobalManager.Instance.GaugesValues[1].ActualValue = 0;
    }

    public void Clean()
    {
        CurrentSoldiers.Clear();

        for (int i = 0; i < CurrentSoldiers.Count; i++)
            CurrentInfoSoldiers[i].gameObject.SetActive(false);

        CurrentTent = null;
    }

    private void Update()
    {
        //TapOrLongPress();

        if (Gamepad.current.startButton.wasPressedThisFrame && CanOpenMap)
        {
            if (DayManager.Instance._isTuto == false && DaytimePlayerCtrler.Instance.isDriving == true)
            {
                _map.SetActive(!_map.activeSelf);
            }
            else
            {
                if (DayManager.Instance._isTuto && TutoManager.Instance.IndexTuto == 12)
                {
                    _map.SetActive(true);
                    TutoManager.Instance.IndexTuto = 13;
                }
            }
        }

        if (DayManager.Instance._isTuto && TutoManager.Instance.IndexTuto >= 13)
        {
            if (Gamepad.current.leftStick.up.wasPressedThisFrame)
            {
                indexTent++;
                if (indexTent > tentsTuto.Count - 1)
                    indexTent = 0;
            }

            if (Gamepad.current.leftStick.down.wasPressedThisFrame)
            {
                indexTent--;
                if (indexTent < 0)
                    indexTent = tentsTuto.Count - 1;
            }

            MarqueurSelection.GetComponent<RectTransform>().position =
                tentsTuto[indexTent].position;

            if (Marked == false)
            {
                if (Gamepad.current.buttonSouth.wasPressedThisFrame)
                {
                    Marked = true;
                    MarqueurTent.SetActive(true);
                    MarqueurTent.GetComponent<RectTransform>().position = new Vector3(
                        MarqueurSelection.GetComponent<RectTransform>().position.x + 15,
                        MarqueurSelection.GetComponent<RectTransform>().position.y + 30, 0);

                    if (indexTent == 2)
                    {
                        if (TutoManager.Instance.IndexTuto == 13)
                        {
                            StartCoroutine(CloseMap());
                            TutoManager.Instance.IndexTuto = 14;
                        }
                    }
                }
            }
            else
            {
                if (Gamepad.current.buttonEast.wasPressedThisFrame)
                {
                    Marked = false;
                    MarqueurTent.SetActive(false);
                }
            }
        }
    }

    IEnumerator CloseMap()
    {
        yield return new WaitForSeconds(1f);
        _map.SetActive(false);
    }
}