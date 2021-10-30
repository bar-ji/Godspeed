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
    [SerializeField] private TMP_Text stateText;
    private TMP_Text velocityText;
    private Image crosshair;

    private void Awake()
    {
        var ui = Instantiate(UI, Vector3.zero, quaternion.identity);
        velocityText = ui.GetComponent<UIReferences>().velocityTxt;
    }
    private void Update()
    {
        float maxSpeed = playerMovement.currentMaxSpeed;
        if (rb.velocity.magnitude > maxSpeed)
            velocityText.text = "Speed: " + maxSpeed.ToString("F2");
        else
            velocityText.text = "Speed: " + new Vector2(rb.velocity.x,rb.velocity.z).magnitude.ToString("F2");

        //stateText.text = playerMovement.currentState.ToString();
    }
}
