using System.Collections.Generic;
using UnityEngine;

public class ArcadeCar : MonoBehaviour
{
    public bool CanHeal;
    public bool Healing;
    public float speed = 10.0f;
    public float CurrentSpeed;
    public float turnSpeed = 10.0f;
    public float CurrentTurnSpeed;

    private Rigidbody rb;
    [SerializeField] private float centerOfMass;
    [SerializeField] private GameObject backWheels;
    [SerializeField] private List<GameObject> frontWheels;

    public Camp CurrentCamp;
    public List<Soldier> soldiers;
    public List<float> HealTime;

    public GameObject SoldierCardPanel;
    public GameObject CamPlayer;

    public GameObject SoldierDebugPanel;

    public bool _mapOpened;
    public GameObject mapCam;
    public GameObject CompassUI;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, centerOfMass, 0);
        CurrentSpeed = speed;
        CurrentTurnSpeed = turnSpeed;
    }

    public void ResetSpeed()
    {
        CurrentSpeed = speed;
        CurrentTurnSpeed = turnSpeed;
    }

    void Update()
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SoldierDebugPanel.SetActive(!SoldierDebugPanel.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!_mapOpened)
            {
                mapCam.SetActive(true);
                CamPlayer.SetActive(false);
                CompassUI.SetActive(false);
                _mapOpened = true;
                CurrentSpeed = 0;
                CurrentTurnSpeed = 0;
            }
            else
            {
                mapCam.SetActive(false);
                CamPlayer.SetActive(true);
                CompassUI.SetActive(true);
                _mapOpened = false;
                ResetSpeed();
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (CanHeal && CurrentCamp.SoldierInPlace != 0)
            {
                CurrentCamp.StartHeal();
                SoldierCardPanel.SetActive(true);
                CamPlayer.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Healing)
            {
                Healing = false;
                CurrentCamp.cam.gameObject.SetActive(false);
                SoldierCardPanel.SetActive(false);
                CamPlayer.SetActive(true);
                ResetSpeed();
            }
        }

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
        }
    }
}