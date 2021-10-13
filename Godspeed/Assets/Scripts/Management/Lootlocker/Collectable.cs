using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using LootLocker.Requests;
using UnityEngine;
using UnityEngine.Networking;
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
                //StartCoroutine(DownloadImage(response.mainItem.files[0].url));
            }
            else
                print("Error triggering collectable" + response.Error);
        });
        
        collectablesManager.UpdateText();
    }

    IEnumerator DownloadImage(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        if(request.isNetworkError || request.isHttpError)
            print(request.error);
        else
        {
            var tex = ((DownloadHandlerTexture) request.downloadHandler).texture;
            collectablesManager.UpdateTexture(tex);
        }
        
    }
}
