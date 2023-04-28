using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    #region Init. Variables
    [Header("Player")]
    public ArcadeCar ArcadeCar;
    public Camera CamPlayer;

    [Space(10)]
    [Header("Camp")]
    public Camp CurrentCamp;
    //[HideInInspector] public CampTuto CampTuto;

    public bool CanHeal;
    public bool Diagnosing;
    public Radio _radio;
    public bool OnBilan;

    [Space(10)]
    [Header("Soldier")]
    public GameObject SoldierCardPanel;
    public GameObject FicheBilan;
    public List<GameObject> SoldierSaved;
    public List<GameObject> SoldierDead;

    [Space(10)]
    [Header("Map")]
    [SerializeField] private Camera _mapCam;
    [SerializeField] private GameObject _mapMarkerPrefab;
    [SerializeField] private GameObject _mapMarker;
    public GameObject _mapMarkerObject;
    [SerializeField] private GameObject CompassUI;
    public bool _mapOpened;

    [Space(10)]
    [Header("Input")]
    float pressTime = 0;
    bool isLongPress = false;
    #endregion

    void Start()
    {
        ArcadeCar = GetComponent<ArcadeCar>();

        if (GameData.NumberDays == 1 && GameData.Started == false)
        {
            GameData.Started = true;
            GameData.speed = 20;
            GameData.turnSpeed = 60;
        }
        else
            GameData.Started = false;

        Cursor.visible = false;
        CamPlayer.enabled = true;
        _mapCam.enabled = false;
    }

    private void Update()
    {
        TapOrLongTouch();

        if (Gamepad.current.yButton.wasReleasedThisFrame && Diagnosing == false)
            MapManager();

        if (Gamepad.current.aButton.wasReleasedThisFrame)
            AButton();

        if (Gamepad.current.bButton.wasReleasedThisFrame)
            BButton();

        if (Diagnosing)
            CompassUI.SetActive(false);
        else
            CompassUI.SetActive(true);
    }

    #region Action
    private void AButton()
    {
        if (Diagnosing && _radio.ActualBullet != null)
        {
            if (isLongPress)
            {
                //_radio.ValidBullet();
                CurrentCamp.NbBulletFound++;
            }
        }

        if (CanHeal && CurrentCamp.IsDiagnostised == false && !_mapOpened && Diagnosing == false)
        {
            CurrentCamp.StartHeal();
            SoldierCardPanel.SetActive(true);
            CamPlayer.enabled = false;
        }

        if (_mapOpened && _mapMarkerObject == null)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 objectPos = _mapCam.ScreenToWorldPoint(mousePos);
            GameObject actualMarker = Instantiate(_mapMarkerPrefab, objectPos, Quaternion.identity);
            _mapMarkerObject = actualMarker;
            _mapMarker.SetActive(true);
            _mapMarker.transform.position = new Vector3(_mapMarkerObject.transform.position.x, 270,
                _mapMarkerObject.transform.position.z);
        }
    }

    private void BButton()
    {
        if (OnBilan)
        {
            FicheBilan.SetActive(false);
        }

        if (Diagnosing == false)
        {
            if (_mapOpened && _mapMarkerObject != null)
            {
                Destroy(_mapMarkerObject);
                _mapMarker.SetActive(false);
            }
        }
        else
        {
            if (isLongPress)
            {
                CurrentCamp.NextSoldier();
            }
        }
    }

    private void MapManager()
    {
        if (!_mapOpened)
        {
            Cursor.visible = true;
            _mapCam.enabled = true;
            CamPlayer.enabled = false;
            CompassUI.SetActive(false);
            _mapOpened = true;

            //Speed 0
            ArcadeCar.enabled = false;
            //

        }
        else
        {
            Cursor.visible = false;
            _mapCam.enabled = false;
            CamPlayer.enabled = true;
            CompassUI.SetActive(true);
            _mapOpened = false;
            ResetSpeed();
        }
    }

    #endregion

    public void ResetSpeed()
    {
        CamPlayer.enabled = true;
        ArcadeCar.enabled = true;
    }

    private void TapOrLongTouch()
    {
        if (Gamepad.current.aButton.isPressed || Gamepad.current.bButton.isPressed)
        {
            pressTime += Time.deltaTime;

            if (pressTime >= 0.5f)
                isLongPress = true;
            else
                isLongPress = false;
        }
        else if (Gamepad.current.aButton.wasReleasedThisFrame || Gamepad.current.bButton.wasReleasedThisFrame)
        {
            pressTime = 0;
        }
    }
}