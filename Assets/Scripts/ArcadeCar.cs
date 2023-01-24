using UnityEngine;

public class ArcadeCar : MonoBehaviour
{
    public bool CanHeal;
    public bool OnHealingMenu;
    public float speed = 10.0f;
    public float CurrentSpeed = 0;
    public float turnSpeed = 10.0f;
    public float suspensionHeight = 0.2f;

    private Rigidbody rb;
    private float inputX;
    private float inputZ;

    public Camp CurrentCamp;
    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        CurrentSpeed = speed;
    }

    public void ResetSpeed()
    {
        CurrentSpeed = speed;
    }
    
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");
        
        if(inputZ < 0)
        {
            inputX = -inputX;
        }
        else if (inputZ == 0)
        {
            inputX = 0;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (CanHeal)
            {
                CurrentCamp.StartHealing();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (OnHealingMenu)
            {
                OnHealingMenu = false;
                CurrentCamp.HealingPanel.SetActive(false);

                ResetSpeed();
            }
        }

        if (OnHealingMenu)
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

    void FixedUpdate()
    {
        rb.velocity = transform.forward * inputZ * CurrentSpeed;
        rb.angularVelocity = transform.up * inputX * turnSpeed;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, suspensionHeight))
        {
            rb.AddForceAtPosition(transform.up * (suspensionHeight - hit.distance) * 800.0f, hit.point);
        }
    }
}