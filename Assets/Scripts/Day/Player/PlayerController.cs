using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool CanHeal;
    public bool Diagnosing;
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

    public GameObject SoldierCardPanel;
    public Camera CamPlayer;

    public bool _mapOpened;
    [SerializeField] Camera mapCam;
    [SerializeField] GameObject CompassUI;
    [SerializeField] GameObject _mapMarkerPrefab;
    public GameObject _mapMarkerObject;
    [SerializeField] private GameObject _mapMarker;

    #region Input
    float pressTime = 0;
    bool isLongPress = false;
    void TapOrLongTouch()
    {
        if (Input.GetKey(KeyCode.F))
        {
            pressTime += Time.deltaTime;

            if (pressTime >= 0.5f)
                isLongPress = true;
            else
                isLongPress = false;
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            if (isLongPress)
            {
                Debug.Log("Long press detected");
            }
            else
            {
                Debug.Log("Short press detected");
            }

            pressTime = 0;
        }
    }
    #endregion


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

        if (Input.GetKeyDown(KeyCode.M))
            MapManager();

        if (Input.GetKeyDown(KeyCode.F))
            if(Diagnosing == false)
                AButton();

        if (Input.GetKeyDown(KeyCode.R))
            BButton();

        if (Diagnosing)
            CompassUI.SetActive(false);
        else
            CompassUI.SetActive(true);
    }

    public void ResetSpeed()
    {
        CurrentSpeed = speed;
        CurrentTurnSpeed = turnSpeed;
    }

    private void AButton()
    {
        if (CanHeal && CurrentCamp.IsDiagnostised == false && !_mapOpened)
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
}