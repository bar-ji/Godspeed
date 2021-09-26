using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class ActiveStateButton : MonoBehaviour
    {
        [SerializeField] private GameObject target;

        private bool isEnabled = false;

        private void Awake() => isEnabled = target.activeSelf;
        public void OnClick()
        {
            if(isEnabled) DisableTarget();
            else EnableTarget();
        }

        private void EnableTarget()
        {
            target.SetActive(true);
            isEnabled = true;
        }
        
        private void DisableTarget()
        {
            target.SetActive(false);
            isEnabled = false;
        }
    }
}

