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

    void Update() {
        var t = transform;
        Vector3 portalToPlayer = player.position - t.position;
        float dotProduct = Vector3.Dot(t.forward, portalToPlayer);

        if (!Physics.CheckSphere(transform.position, 1f, playerLayer)) return;
        
        if(dotProduct < 0f)
        {
            player.position = destination.position + portalToPlayer;
        }
    }
}
