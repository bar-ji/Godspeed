using DG.Tweening;
using UnityEngine;

namespace Tweeners
{
    public class TweenToVector : MonoBehaviour
    {
        [SerializeField] private float posMultiplier = 1;
        [SerializeField] private float rotMultiplier = 1;
        [SerializeField] private float scaleMultiplier = 1;
        private Vector3 awakePos;
        private Vector3 awakeRot;
        private Vector3 awakeScale;

        [SerializeField] private float time;
        [SerializeField] private Ease easeEndType;
        [SerializeField] private Ease easeStartType;

        void Awake()
        {
            Transform t = transform;
            
            awakePos = t.localPosition;
            awakeRot = t.localEulerAngles;
            awakeScale = t.localScale;
        }

        public void TweenAllEnd()
        {
            transform.DOLocalMove(awakePos * posMultiplier, time).SetEase(easeEndType);
            transform.DOLocalRotate(awakeRot * rotMultiplier, time).SetEase(easeEndType);
            transform.DOScale(awakeScale * scaleMultiplier, time).SetEase(easeEndType);
        }

        public void TweenAllStart()
        {
            transform.DOLocalMove(awakePos, time).SetEase(easeStartType);
            transform.DOLocalRotate(awakeRot, time).SetEase(easeStartType);
            transform.DOScale(awakeScale, time).SetEase(easeStartType);
        }



    }
}

