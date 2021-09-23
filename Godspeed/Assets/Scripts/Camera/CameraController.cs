using System;
using System.Collections;
using System.Collections.Generic;
using Management;
using UnityEngine;

namespace Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private new Transform camera;
        [SerializeField] private Transform target;
        [SerializeField] private Transform orientation;
        [SerializeField] private Vector2 sensitivity;
        private float xRot;
        private float yRot;
        private bool isLocked;
    
        float mouseX;
        float mouseY;

        private void Start()
        {
            GameState.instance.OnEscapePressed += SetIsLocked;
            Cursor.lockState = CursorLockMode.Locked;
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

        public void SetIsLocked()
        {
            isLocked = !isLocked;
            Cursor.lockState = isLocked ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
}


