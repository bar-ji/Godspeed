using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnZeroScale : MonoBehaviour
{
    void Update()
    {
        if (transform.localScale.x <= 0)
        {
            Destroy(gameObject);
        }
    }
}
