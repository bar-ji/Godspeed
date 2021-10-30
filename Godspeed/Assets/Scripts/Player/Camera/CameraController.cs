using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Management;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Cam
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Transform orientation;
        [SerializeField] private Vector2 sensitivity;
        private InputManager inputManager;
        private float xRot;
        public float yRot { get; set; }

        private float mouseX;
        private float mouseY;

        private void Start()
        {
            inputManager = InputManager.instance;
        }

        private void Update()
        {
            transform.position = target.position;
            
            mouseX = inputManager.xMouse * sensitivity.x;
            mouseY = inputManager.yMouse * sensitivity.y;
            
            float ClampedRot(float x) => Mathf.Clamp(x, -85f, 85f);
            
            xRot -= mouseY;
            xRot = ClampedRot(xRot);
            yRot += mouseX;
            
            //This is used to rotate the camera.
            transform.localRotation = Quaternion.Euler(xRot, yRot, 0);
            //This is used as the direction for the player.
            orientation.localRotation = Quaternion.Euler(0, yRot, 0);
        }
    }
}


