using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace World
{
    public sealed class Oscillator : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float maxOffset;
        [SerializeField] private OscillateType type;
        [SerializeField] private bool randomSpeed;
        
        
        private float t;
        private float start;
        private float end;
        private bool isIncrementing = true;

        private void Start()
        {
            switch (type)
            {
                case OscillateType.YPosition:
                    start = transform.localPosition.y;
                    break;
                case OscillateType.XRotation:
                    start = transform.localEulerAngles.y;
                    break;
                case OscillateType.Scale:
                    start = transform.localScale.y;
                    break;
            }
            end = start + maxOffset;

            if (randomSpeed)
                speed = Random.Range(speed / 2, speed * 2);
        }

        private void Update()
        {
            Incrementor();
        }
        
        private void LateUpdate()
        {
            Vector3 current;

            switch (type)
            {
                case OscillateType.YPosition:
                    current = transform.localPosition;
                    current.y = Mathf.SmoothStep(start, end, t);
                    transform.localPosition = current;
                    break;
                case OscillateType.XRotation:
                    current = transform.localEulerAngles;
                    current.y = Mathf.SmoothStep(start, end, t);
                    transform.localEulerAngles = current;
                    break;
                case OscillateType.Scale:
                    current = transform.localScale;
                    current.x = Mathf.SmoothStep(start, end, t);
                    current.y = Mathf.SmoothStep(start, end, t);
                    current.z = Mathf.SmoothStep(start, end, t);
                    transform.localScale = current;
                    break;
            }
        }

        protected void Incrementor()
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
    }

    enum OscillateType
    {
        YPosition, XRotation, Scale
    }
}

