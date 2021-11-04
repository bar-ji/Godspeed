using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using Unity.Mathematics;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerGraphics : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Rigidbody rb;
    private TMP_Text speedTxt;
    private Image crosshair;

    private void Start()
    {
        var ui = Instantiate(UI, Vector3.zero, quaternion.identity);
        speedTxt = DebugMenu.instance.speedTxt;
    }
    private void Update()
    {
        float maxSpeed = playerMovement.currentMaxSpeed;
        if (rb.velocity.magnitude > maxSpeed)
            speedTxt.text = "Speed: " + maxSpeed.ToString("F2");
        else
            speedTxt.text = "Speed: " + new Vector2(rb.velocity.x,rb.velocity.z).magnitude.ToString("F2");

        //stateText.text = playerMovement.currentState.ToString();
    }
}
