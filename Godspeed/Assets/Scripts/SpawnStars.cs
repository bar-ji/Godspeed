using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnStars : MonoBehaviour
{
    [SerializeField] private GameObject star;
    [SerializeField] private int amount;
    [SerializeField] private float radius;

    private void Awake()
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 offset = Random.insideUnitSphere.normalized;
            offset *= radius;
            var starObj = Instantiate(star, transform.position + offset, Quaternion.identity);
            starObj.transform.localScale *= Random.Range(0.2f, 1);
            starObj.transform.parent = transform;
        }
    }
}
