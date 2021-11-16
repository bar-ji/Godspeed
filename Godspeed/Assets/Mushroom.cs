using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public Transform destination;
    public Vector3 direction {
        get
        {
            transform.DOPunchScale(Vector3.one / 5, .4f).SetEase(Ease.OutExpo);
            return (destination.position - transform.position).normalized;
        }
        set { }
    }
    
}
