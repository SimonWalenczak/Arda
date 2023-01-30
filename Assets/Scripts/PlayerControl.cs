using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl Instance;

    [Header("Values")]
    [SerializeField] private float Speed;
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float TurnSpeed = 0.03f;
    [SerializeField] private float BoingForce;
    [SerializeField] private float MaxBoingForce;
    [SerializeField] private float MinBoingForce;
    [SerializeField] private float camTurnSpeed;
    [SerializeField] private Transform Cam;
    [SerializeField] private LayerMask WhatIsGround;

    [Header("References")]
    [SerializeField] private Transform groundCheckForward, groundCheckBack; 
    [SerializeField] private Transform raycaster;
    [SerializeField] private GameObject CarMesh;
    private Vector3 forward, right;
    private Rigidbody rb;
    private bool Grounded;
    private float OriginalX;
    private Quaternion offset;
    float YInput, XInput;

    RaycastHit slopeHit, groundHit;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        OriginalX = transform.rotation.x;
    }

    void Update()
    {
        InitializeMoveDir();

        Grounded = Physics.CheckCapsule(groundCheckForward.position, groundCheckBack.position, 0.1f, WhatIsGround);

        if (Grounded && Input.GetKeyDown(KeyCode.Space))
        {
            var Boing = BoingForce * rb.velocity.magnitude;
            if (Boing < MinBoingForce)
                Boing = MinBoingForce;

            if (Boing > MaxBoingForce)
                Boing = MaxBoingForce;

            rb.AddForce(Vector3.up * Boing);
        }

        YInput = Input.GetAxis("Vertical");
        XInput = Input.GetAxis("Horizontal") * 1.3f;


        Cam.Rotate(0, camTurnSpeed * XInput * Time.deltaTime, 0);

        if (Grounded)
        {
            if (Mathf.Abs(YInput) > 0.1f)
            {
                transform.forward = Vector3.Lerp(transform.forward, Cam.forward, TurnSpeed);
            }
        }
        else
            transform.forward = Vector3.Lerp(transform.forward, rb.velocity, TurnSpeed);

    }

    void FixedUpdate()
    {
        Physics.Raycast(raycaster.position, -raycaster.up, out groundHit, 2f, WhatIsGround);

        if (Mathf.Abs(YInput) > 0.1f && Grounded)
        {
            rb.AddForce(transform.forward * Speed * YInput);

            if (rb.velocity.magnitude > MaxSpeed)
            {
                Vector3 Dir = Vector3.Normalize(rb.velocity);
                rb.velocity = Dir * MaxSpeed;
            }
        }
    }



    void InitializeMoveDir()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }
}