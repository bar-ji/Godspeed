using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenCanvasOpacity : MonoBehaviour
{
    [SerializeField] private float maxOpacity;
    [SerializeField] private float minOpacity;
    [SerializeField] private float time;
    [SerializeField] private Ease easeEnd;
    [SerializeField] private Ease easeStart;
    [SerializeField] private CanvasGroup group;

    public void TweenEnd()
    {
        group.DOFade(maxOpacity, time).SetEase(easeEnd);
    }

    public void TweenStart()
    {
        group.DOFade(minOpacity, time).SetEase(easeStart);
    }
}
