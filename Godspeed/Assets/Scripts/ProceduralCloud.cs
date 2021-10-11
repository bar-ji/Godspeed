using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class ProceduralCloud : MonoBehaviour
{
    [Tooltip("Angle at which to spawn the cloud on the unit circle.")]
    [SerializeField] private float angle;
    [Tooltip("Size of the unit circle on which to spawn the cloud.")]
    [SerializeField] private float radius;
    [Tooltip("Speed at which the clouds animate.")]
    [SerializeField] private float speed;
    [Tooltip("How far apart the clouds spawn (inverse density).")]
    [SerializeField] private float sparsity;
    [Tooltip("Amount of clouds to spawn.")]
    [SerializeField] private int amount;
    [Tooltip("Time between each cloud spawned")]
    [SerializeField] private float delay;
    [Tooltip("Max size to which the clouds will scale up to")]
    [SerializeField] private float maxSize;
    [Tooltip("Min size to which the clouds will scale up to")]
    [SerializeField] private float minSize;
    [SerializeField] private GameObject cloud;
    private Vector3 position;
    
    

    private IEnumerator Start()
    {
        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
        float z = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
        position = new Vector3(x, transform.position.y, z);
        transform.position += position;

        for (int i = 0; i < amount; i++)
        {
            yield return new WaitForSeconds(delay);
            SpawnCloud();
        }
    }

    void SpawnCloud()
    {
        Vector3 offset = Random.insideUnitSphere * sparsity;
        var cloudObj = Instantiate(cloud, transform.position + offset, Quaternion.identity);
        cloudObj.transform.parent = transform;
        CloudTweener tweener = cloudObj.GetComponent<CloudTweener>();
        tweener.time *= speed;
        tweener.maxScale = Random.Range(minSize, maxSize);
        tweener.procCloud = this;
        tweener.ScaleUp();
    }

    public void DelaySpawn()
    {
        StartCoroutine(DelayedSpawn());
    }

    public IEnumerator DelayedSpawn()
    {
        yield return new WaitForSeconds(delay);
        SpawnCloud();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
