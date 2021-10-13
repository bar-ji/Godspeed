using System;
using Cam;
using Player;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform destination;
    [SerializeField] private CameraController controller;

    [SerializeField] private LayerMask playerLayer;

    private bool overlapping;

    void Update() {
        var t = transform;
        Vector3 portalToPlayer = player.position - t.position;
        float dotProduct = Vector3.Dot(t.forward, portalToPlayer);

        if (!overlapping) return;
        
        if(dotProduct < 0f)
        {
            player.position = destination.position + portalToPlayer;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        overlapping = true;
    }

    private void OnTriggerExit(Collider other)
    {
        overlapping = false;
    }
}
