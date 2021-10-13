using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using LootLocker.Requests;
using LootLocker;
using UnityEngine;
using Steamworks;

public class AuthenticatePlayer : MonoBehaviour
{
    private void Awake()
    {
        
        if(SteamManager.Initialized) {
            string name = SteamFriends.GetPersonaName();
            Debug.Log(name);
        }
    }

    private void Start()
    {
        string playerID = "Master";
        LootLockerSDKManager.StartSession(playerID, (response) =>
        {
            if (response.success)
            {
                print("Connected to lootlocker successfully!");
            }
            else
            {
                print("Failed: " + response.Error);
            }
        });
    }
}
