using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Star : MonoBehaviour
{
    private Vector3 startScale;
    private Vector3 endScale;

    private float randomTime;

    private void Start()
    {
        randomTime = Random.Range(3, 6);
        startScale = transform.localScale;
        endScale = startScale / 2;
        StartCoroutine(ScaleDown());
    }

    IEnumerator ScaleUp()
    {
        transform.DOScale(endScale, randomTime).SetEase(Ease.InOutBack);
        yield return new WaitForSeconds(randomTime);
        StartCoroutine(ScaleDown());
    }
    
    IEnumerator ScaleDown()
    {
        transform.DOScale(startScale, randomTime).SetEase(Ease.InOutBack);
        yield return new WaitForSeconds(randomTime);
        StartCoroutine(ScaleUp());
    }
    
}
