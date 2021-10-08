using System;
using UnityEngine;

public class Float : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxOffset;
    private float t;
    private float startPos;
    private float endPos;

    private bool isIncrementing = true;

    private void Start()
    {
        startPos = transform.localPosition.y;
        endPos = startPos + maxOffset;
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
        Vector3 pos = transform.localPosition;
        pos.y = Mathf.SmoothStep(startPos, endPos, t);
        transform.localPosition = pos;
    }
}
