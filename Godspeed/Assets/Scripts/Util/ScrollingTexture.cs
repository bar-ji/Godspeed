using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingTexture : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector2 direction;
    [SerializeField] private Material mat;


    void Update()
    {
        mat.mainTextureOffset += direction * speed * Time.deltaTime;
    }
}
