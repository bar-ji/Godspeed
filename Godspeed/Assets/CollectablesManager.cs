using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class CollectablesManager : MonoBehaviour
{
    [SerializeField] private TMP_Text collectablesTxt;

    public static CollectablesManager instance;

    private int amountOfCards = 0;

    [SerializeField] private Material mat;

    private void Awake()
    {
        if(instance) Destroy(this);
        instance = this;
    }

    public void UpdateText()
    {
        amountOfCards++;
        collectablesTxt.text = "Collectables Found = " + amountOfCards;
    }
    
    public void UpdateTexture(Texture2D tex)
    {
        mat.SetTexture("_MainTex", tex);
    }


    public int GetNumberHubCardsCollected()
    {
        int amount = 0;
        LootLockerSDKManager.GettingCollectables((response) =>
        {
            if (response.success)
                amount = response.collectables.Length;
            else
            {
                print("Error getting collectables: " + response.Error);
                amount = -1;
            }
        });
        return amount;
    }
}
