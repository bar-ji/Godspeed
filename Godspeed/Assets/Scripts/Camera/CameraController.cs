using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wildflare.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private new Transform camera;
        [SerializeField] private Transform target;
        [SerializeField] private Transform orientation;
        [SerializeField] private Vector2 sensitivity;
        private float xRot;
        private bool isLocked;
    
        float mouseX;
        float mouseY;

        private void Start()
        {
            SetIsLocked(false);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                SetIsLocked(!isLocked);

            mouseX = Input.GetAxisRaw("Mouse X") * sensitivity.x * Time.deltaTime;
            mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity.y * Time.deltaTime;
        }

        private void LateUpdate()
        {
            transform.position = target.position;
        
            if (isLocked) return;
            
            xRot -= mouseY * Time.deltaTime;
            xRot = Mathf.Clamp(xRot, -90.0f, 90.0f);
            
            camera.localRotation = Quaternion.Euler(xRot, 0, 0);
            transform.Rotate(Vector3.up * mouseX);
            //This is used as the direction for the player.
            orientation.Rotate(Vector3.up * mouseX);
        }

        public void SetIsLocked(bool value)
        {
            isLocked = value;
            Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
}


