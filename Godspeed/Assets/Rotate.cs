using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxOffset;
    private float t;
    private float startRot;
    private float endRot;

    private bool isIncrementing = true;

    private void Start()
    {
        startRot = transform.localEulerAngles.y;
        endRot = startRot + maxOffset;
    }

    private void Update()
    {
        if (t >= 1)
            isIncrementing = false;

        if (t <= 0)
            isIncrementing = true;

        if (isIncrementing)
            t += Time.deltaTime * speed;
        else
            t -= Time.deltaTime * speed;
    }

    private void LateUpdate()
    {
        Vector3 rot = transform.localEulerAngles;
        rot.y = Mathf.SmoothStep(startRot, endRot, t);
        transform.localEulerAngles = rot;
    }
}
