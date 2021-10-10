using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;


public class ProceduralCloud : MonoBehaviour
{
    [SerializeField] private float angle;
    [SerializeField] private float height;
    [SerializeField] private float radius;
    [SerializeField] private float speed;
    [SerializeField] private float inverseDensity;
    [SerializeField] private int amount;
    [SerializeField] private GameObject cloud;
    private Vector3 position;

    private List<GameObject> activeClouds = new List<GameObject>();
    private int cloudsSpawned;

    private void Start()
    {
        SpawnCloud();
    }

    void SpawnCloud()
    {
        position.x = Mathf.Cos(Mathf.Deg2Rad * angle);
        position.z = Mathf.Sin(Mathf.Deg2Rad * angle);
        position *= radius;
        position.y = height;
        var cloudObj = Instantiate(cloud, position, quaternion.identity);
        cloudObj.transform.parent = transform;
        activeClouds.Add(cloudObj);
        
        cloudObj.transform.DOScale(Vector3.one * 6, (1 / speed) * amount).SetEase(Ease.OutSine);
        if(cloudsSpawned > amount - 2)
            activeClouds[cloudsSpawned - (amount - 1)].transform.DOScale(Vector3.zero, 1 / speed).SetEase(Ease.InSine);
        if (cloudsSpawned > amount)
        {
            activeClouds.RemoveAt(cloudsSpawned - amount - 1);
            cloudsSpawned--;
        }
            
        angle += inverseDensity;
        cloudsSpawned++;
        StartCoroutine(Tick());
    }

    IEnumerator Tick()
    {
        yield return new WaitForSeconds(1 / speed);
        SpawnCloud();
    }
}
