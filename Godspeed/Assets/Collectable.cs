using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using LootLocker.Requests;
using UnityEngine;
using World;
using Random = UnityEngine.Random;

public class Collectable : MonoBehaviour
{
    [SerializeField] private float animateTime;
    [SerializeField] private int cardNumber;

    private CollectablesManager collectablesManager;

    private void Awake() => collectablesManager = CollectablesManager.instance;

    public void AnimateOut()
    {
        foreach (var osc in GetComponents<Oscillator>())
            osc.enabled = false;
        transform.DOScale(Vector3.zero, animateTime).SetEase(Ease.InCirc);
        transform.DOLocalRotate(Random.insideUnitSphere * 359, animateTime).SetEase(Ease.InCirc);
    }

    public void Collected()
    {
        LootLockerSDKManager.CollectingAnItem("Trading_Cards.Hub.HubCard" + cardNumber, (response) =>
        {
            if (response.success)
            {
                print("Successfully collected collectable");
                collectablesManager.UpdateTexture(response.texture);
            }
            else
                print("Error triggering collectable" + response.Error);
        });
        
        collectablesManager.UpdateText();
    }
}
