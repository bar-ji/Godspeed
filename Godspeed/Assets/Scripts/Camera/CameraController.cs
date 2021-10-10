using System;
using System.Collections;
using System.Collections.Generic;
using Management;
using UnityEngine;

namespace Cam
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private new Transform camera;
        [SerializeField] private Transform target;
        [SerializeField] private Transform orientation;
        [SerializeField] private Vector2 sensitivity;
        private float xRot;
        public float yRot { get; set; }

        private float mouseX;
        private float mouseY;

        private bool isLocked;

        private void Start()
        {
            EventHandlerSystem eventHandler = EventHandlerSystem.instance;
            eventHandler.OnPause += SetIsLocked;
            eventHandler.OnUnpause += SetIsLocked;
        }
        private void Update()
        {
            mouseX = Input.GetAxisRaw("Mouse X") * sensitivity.x;
            mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity.y;
            
        }

        private void LateUpdate()
        {
            transform.position = target.position;
        
            if (isLocked) return;

            xRot -= mouseY * 0.02f; 
            xRot = Mathf.Clamp(xRot, -90.0f, 90.0f);
            yRot += mouseX * 0.02f;

            camera.localRotation = Quaternion.Euler(xRot, 0, 0);
            transform.localRotation = Quaternion.Euler(0, yRot, 0);
            //This is used as the direction for the player.
            orientation.localRotation = Quaternion.Euler(0, yRot, 0);
            
        }

        private void SetIsLocked()
        {
            isLocked = !isLocked;
        }
        
        public void OffsetRotation(float offset) {
            yRot += offset;
            transform.rotation = Quaternion.Euler(0, yRot, 0);
        }
    }
}


