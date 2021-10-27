using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{ 
    [RequireComponent(typeof(EventTrigger))]
    public class AnimatedButton : MonoBehaviour, IButton
    {
                         private Vector2 startScale;
                         private Vector2 endScale;
        [SerializeField] private Vector2 scaleMultiplier = new Vector2(1.2f, 1.2f);
        [SerializeField] private float scaleTime = 0.5f;
        [SerializeField] private Ease upEase;
        [SerializeField] private Ease downEase;

        private void Awake()
        {
            startScale = transform.localScale;
            endScale = startScale * scaleMultiplier;
        }

        public void OnCursorEnter()
        {
            transform.DOScale(endScale, scaleTime).SetEase(upEase);
        }

        public void OnCursorExit()
        {
            transform.DOScale(startScale, scaleTime).SetEase(downEase);
        }

        public void OnClick()
        {
            transform.DOPunchScale(endScale * 0.1f, scaleTime / 2, 2, 0f);
        }
    }
}

