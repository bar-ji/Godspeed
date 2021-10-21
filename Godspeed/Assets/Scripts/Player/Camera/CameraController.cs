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
        [SerializeField] private new Transform camera;
        [SerializeField] private Transform target;
        [SerializeField] private Transform orientation;
        [SerializeField] private Vector2 sensitivity;
        [SerializeField] private PostProcessProfile postProcessProfile;
        private float xRot;
        public float yRot { get; set; }

        private float mouseX;
        private float mouseY;

        private bool isLocked = true;
        
        DepthOfField dof;

        private void Start()
        {
            SetIsLocked();
            postProcessProfile.TryGetSettings(out dof);
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

            float RotOffset(float x) => x * 0.02f;
            float ClampedRot(float x) => Mathf.Clamp(x, -90f, 90f);

            xRot -= RotOffset(mouseY);
            xRot = ClampedRot(xRot);
            yRot += RotOffset(mouseX);

            camera.localRotation = Quaternion.Euler(xRot, 0, 0);
            transform.localRotation = Quaternion.Euler(0, yRot, 0);
            //This is used as the direction for the player.
            orientation.localRotation = Quaternion.Euler(0, yRot, 0);
        }

        private void SetIsLocked()
        {
            isLocked = !isLocked;
        }

        private void ManageDOF()
        {
            if(!dof) return;
            
            dof.enabled.value = !dof.enabled.value;
            dof.focalLength.value = 30;
            if (dof.enabled.value)
                DOTween.To(() => dof.focalLength.value, x => dof.focalLength.value = x, 60f, 1f).SetEase(Ease.OutBack);
        }

        public void OnPauseStateChanged()
        {
            SetIsLocked();
            ManageDOF();
        }

        public void OffsetRotation(float offset) 
        {
            yRot += offset;
            transform.rotation = Quaternion.Euler(0, yRot, 0);
        }

        private void OnDestroy()
        {
            DepthOfField dof;
            if (postProcessProfile.TryGetSettings(out dof))
            {
                dof.enabled.value = false;
            }
        }
    }
}


