using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeCar : MonoBehaviour
{
    public float speed = 10.0f;
    public float turnSpeed = 10.0f;
    public float suspensionHeight = 0.2f;

    private Rigidbody rb;
    private float inputX;
    private float inputZ;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        // Empêcher le véhicule de tourner si il ne bouge pas
        if(inputZ <= 0)
        {
            inputX = 0;
        }
    }

    void FixedUpdate()
    {
        // Appliquer la vitesse et la rotation en fonction des entrées
        rb.velocity = transform.forward * inputZ * speed;
        rb.angularVelocity = transform.up * inputX * turnSpeed;

        // Appliquer la suspension en faisant rebondir la voiture lorsque la distance entre le centre de la voiture et le sol est inférieure à la hauteur de suspension
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, suspensionHeight))
        {
            rb.AddForceAtPosition(transform.up * (suspensionHeight - hit.distance) * 800.0f, hit.point);
        }
    }
}