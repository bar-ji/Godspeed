using System;
using DG.Tweening;
using UnityEngine;

namespace World.Tweeners
{
    public class TweenToTarget : MonoBehaviour
    {
        [SerializeField] private Transform end;
                         private T start;
                         [SerializeField] private float time;
        [SerializeField] private Ease easingIn;
        [SerializeField] private Ease easingOut;

        private void Start()
        {
            start.localPosition = transform.localPosition;
            start.localEulerAngles = transform.localEulerAngles;
            start.localScale = transform.localScale;
        }

        public void TweenToEnd()
        {
            transform.DOLocalMove(end.localPosition, time).SetEase(easingIn);
            transform.DOLocalRotate(end.localEulerAngles, time).SetEase(easingIn);
            transform.DOScale(end.localScale, time).SetEase(easingIn);
        }
        
        public void TweenToStart()
        {
            transform.DOLocalMove(start.localPosition, time).SetEase(easingOut);
            transform.DOLocalRotate(start.localEulerAngles, time).SetEase(easingOut);
            transform.DOScale(start.localScale, time).SetEase(easingOut);
        }
    }

    struct T
    {
        public Vector3 localPosition;
        public Vector3 localEulerAngles;
        public Vector3 localScale;
    }
}
