using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class CloudTweener : MonoBehaviour
{
    public float time { get; set; }
    public float maxScale { get; set; }
    public ProceduralCloud procCloud { get; set; }

    private void Awake()
    {
        time = Random.Range(3, 10);
    }

    public void ScaleUp()
    {
        transform.DOScale(Vector3.one * maxScale, time).SetEase(Ease.OutCirc);
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(time);
        ScaleDown();
    }

    void ScaleDown()
    {
        transform.DOScale(Vector3.zero, time).SetEase(Ease.InCirc);;
        Invoke(nameof(DestroyDelay), time - 0.01f);
    }

    void DestroyDelay()
    {
        procCloud.DelaySpawn();
    }
    
    
    
}
