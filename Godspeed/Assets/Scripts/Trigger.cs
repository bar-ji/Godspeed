using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [SerializeField] private UnityEvent onTriggerEntered;
    [SerializeField] private UnityEvent onTriggerExit;
    private void OnTriggerEnter(Collider other) => onTriggerEntered.Invoke();
    private void OnTriggerExit(Collider other) => onTriggerExit.Invoke();
}
