using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;
    [SerializeField] private Transform orientation;

    [SerializeField] private TMP_Text velocityText;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Mathf.Abs(rb.velocity.magnitude - maxSpeed) < 0.25f)
            velocityText.text = "VEL: " + maxSpeed;
        else
            velocityText.text = "VEL: " + rb.velocity.magnitude.ToString("F1");
    }   
    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 forwardDir = orientation.forward * acceleration * Input.GetAxisRaw("Vertical");
        Vector3 rightDir = orientation.right * acceleration * Input.GetAxisRaw("Horizontal");
        Vector3 dir = (forwardDir + rightDir).normalized;

        if (rb.velocity.magnitude < maxSpeed)
            rb.AddForce(dir * acceleration, ForceMode.Impulse);
        else
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }
}
