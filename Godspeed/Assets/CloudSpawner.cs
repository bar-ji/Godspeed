using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cloud;
    [SerializeField] private float maxScale;
    [SerializeField] private float minScale;

    [SerializeField] private float time;

    [SerializeField] private float initialDelay;

    private bool scaleUp;

    IEnumerator Start()
    {
        cloud = Instantiate(cloud, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(initialDelay);

        yield return AnimateCloud();
    }

    IEnumerator AnimateCloud()
    {

        if (!scaleUp)
            cloud.transform.DOScale(minScale, time);
        else
            cloud.transform.DOScale(maxScale, time);

        yield return new WaitForSeconds(time);
        scaleUp = !scaleUp;
        yield return AnimateCloud();
    }
}
