using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool CanHeal;
    public bool Healing;
    public float speed = 10.0f;
    public float CurrentSpeed;
    public float turnSpeed = 10.0f;
    public float CurrentTurnSpeed;
    [SerializeField] private Vector3 _centerOfMass;

    private Rigidbody rb;
    [SerializeField] private GameObject backWheels;
    [SerializeField] private List<GameObject> frontWheels;

    public Camp CurrentCamp;
    public List<Soldier> soldiers;
    public List<float> HealTime;

    public GameObject SoldierCardPanel;
    public Camera CamPlayer;

    public GameObject SoldierDebugPanel;

    public bool _mapOpened;
    [SerializeField] Camera mapCam;
    [SerializeField] GameObject CompassUI;
    [SerializeField] GameObject _mapMarkerPrefab;
    public GameObject _mapMarkerObject;
    [SerializeField] private GameObject _mapMarker;
    
    
    void Start()
    {
        if (GameData.NumberDays == 1 && GameData.Started == false)
        {
            GameData.Started = true;
            GameData.speed = 20;
            GameData.turnSpeed = 60;
        }
        else
        {
            GameData.Started = false;
        }
        
        rb = GetComponentInParent<Rigidbody>();
        rb.centerOfMass = _centerOfMass;
        speed = GameData.speed;
        turnSpeed = GameData.turnSpeed;
        CurrentSpeed = speed;
        CurrentTurnSpeed = turnSpeed;
        Cursor.visible = false;
        mapCam.enabled = false;
        CamPlayer.enabled = true;
    }

    void Update()
    {
        Move();
        Heal();

        if (Input.GetKeyDown(KeyCode.Escape))
            SoldierDebug();

        if (Input.GetKeyDown(KeyCode.M))
            MapManager();

        if (Input.GetKeyDown(KeyCode.F))
            AButton();

        if (Input.GetKeyDown(KeyCode.R))
            BButton();
        
        if(Healing)
            CompassUI.SetActive(false);
        else
            CompassUI.SetActive(true);
    }

    public void ResetSpeed()
    {
        CurrentSpeed = speed;
        CurrentTurnSpeed = turnSpeed;
    }

    private void Heal()
    {
        if (Healing)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                CurrentCamp.SelectedSoldier++;
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                CurrentCamp.SelectedSoldier--;
            }
            
            if (CurrentCamp.SoldierInPlace == 0)
                SoldierCardPanel.SetActive(false);
            else
                SoldierCardPanel.SetActive(true);
        }
    }

    private void AButton()
    {
        if (CanHeal && CurrentCamp.SoldierInPlace != 0 && !_mapOpened)
        {
            CurrentCamp.StartHeal();
            SoldierCardPanel.SetActive(true);
            CamPlayer.enabled = false;
        }

        if (_mapOpened && _mapMarkerObject == null)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 objectPos = mapCam.ScreenToWorldPoint(mousePos);
            GameObject actualMarker = Instantiate(_mapMarkerPrefab, objectPos, Quaternion.identity);
            _mapMarkerObject = actualMarker;
            _mapMarker.SetActive(true);
            _mapMarker.transform.position = new Vector3(_mapMarkerObject.transform.position.x, 270,
                _mapMarkerObject.transform.position.z);
        }
    }

    private void BButton()
    {
        if (Healing)
        {
            Healing = false;
            CurrentCamp.cam.gameObject.SetActive(false);
            SoldierCardPanel.SetActive(false);
            CamPlayer.enabled = true;
            CurrentCamp.WaitingForSpawn();
            ResetSpeed();
        }

        if (_mapOpened && _mapMarkerObject != null)
        {
            Destroy(_mapMarkerObject);
            _mapMarker.SetActive(false);
        }
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (vertical != 0)
        {
            transform.position += transform.forward * vertical * CurrentSpeed * Time.deltaTime;

            if (vertical >= 0.1f)
            {
                transform.Rotate(Vector3.up, horizontal * CurrentTurnSpeed * Time.deltaTime);

                backWheels.transform.Rotate(1.0f, 0.0f, 0.0f, Space.Self);

                foreach (var frontWheel in frontWheels)
                    frontWheel.transform.Rotate(1.0f, 0, 0.0f, Space.Self);
            }

            if (vertical <= -0.1f)
            {
                transform.Rotate(Vector3.up, -horizontal * CurrentTurnSpeed * Time.deltaTime);

                backWheels.transform.Rotate(-1.0f, 0.0f, 0.0f, Space.Self);

                foreach (var frontWheel in frontWheels)
                    frontWheel.transform.Rotate(-1.0f, 0, 0.0f, Space.Self);
            }
        }
    }

    private void MapManager()
    {
        if (!_mapOpened)
        {
            Cursor.visible = true;
            mapCam.enabled = true;
            CamPlayer.enabled = false;
            CompassUI.SetActive(false);
            _mapOpened = true;
            CurrentSpeed = 0;
            CurrentTurnSpeed = 0;
        }
        else
        {
            Cursor.visible = false;
            mapCam.enabled = false;
            CamPlayer.enabled = true;
            CompassUI.SetActive(true);
            _mapOpened = false;
            ResetSpeed();
        }
    }

    private void SoldierDebug()
    {
        SoldierDebugPanel.SetActive(!SoldierDebugPanel.activeSelf);
    }
}