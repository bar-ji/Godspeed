using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerGraphics : MonoBehaviour
{
    [SerializeField] private GameObject UI;

    private void Awake()
    {
        Instantiate(UI, Vector3.zero, quaternion.identity);
    }
}
