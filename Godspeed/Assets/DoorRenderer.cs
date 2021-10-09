using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRenderer : MonoBehaviour
{
    [SerializeField] private UnityEngine.Camera playerCamera;
    [SerializeField] private UnityEngine.Camera portalCamera;

    [SerializeField] private Transform source;
    [SerializeField] private Transform destination;

    private Vector3 cameraPositionInSourceSpace => source.InverseTransformPoint(playerCamera.transform.position);
    private Quaternion cameraRotationInSourceSpace => Quaternion.Inverse(source.rotation) * playerCamera.transform.rotation;

    private void Update()
    {
        portalCamera.transform.position = destination.TransformPoint(cameraPositionInSourceSpace);
        portalCamera.transform.rotation = destination.rotation * cameraRotationInSourceSpace;
    }
}
