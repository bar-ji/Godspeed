using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Sphere : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private float dist;
    [SerializeField] private Rigidbody rb;

    private void FixedUpdate()
    {
        if (transform.position.y < dist)
        {
            rb.AddForce(Vector3.up * force);
        }
    }
}
