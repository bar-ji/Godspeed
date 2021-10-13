using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private Vector3 speed;
    private Vector3 currentRot;

    void Update()
    {
        currentRot += Time.deltaTime * speed;
        transform.localEulerAngles = currentRot;
    }
    
}
