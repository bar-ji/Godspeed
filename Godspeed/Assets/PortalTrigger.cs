using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PortalTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent rise;
    [SerializeField] private UnityEvent fall;

    private bool isInside;

    [SerializeField] private Transform player;

    private void OnTriggerEnter(Collider other)
    {
        if (isInside) return;
        isInside = true;
        rise.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        print((transform.position - player.position).magnitude);
        if ((transform.position - player.position).magnitude > 100) return;
        isInside = false;
        fall.Invoke();
    }
}
