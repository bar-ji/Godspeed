using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private float amount;
    [SerializeField] private GameObject cloud;

    void Start()
    {
        for (int i = 1; i <= amount; i++)
        {
            float angle = (i / amount) * 360;
            float xPos = Mathf.Cos(Mathf.Deg2Rad * angle) * range;
            float zPos = Mathf.Sin(Mathf.Deg2Rad * angle) * range;
            Vector3 spawnPos = new Vector3(xPos, transform.position.y, zPos);
            
            GameObject cloudObj = Instantiate(cloud, spawnPos, Quaternion.identity);
            cloudObj.transform.parent = transform;
        }   
    }
}
