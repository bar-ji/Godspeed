using System;
using System.Collections;
using System.Collections.Generic;
using Management;
using TMPro;
using UnityEngine;

public class DebugMenu : MonoBehaviour
{
    public static DebugMenu instance;
    
    [SerializeField] private TMP_Text FPSText;
    [SerializeField] private TMP_Text SpeedTxt;

    //Fix
    
    void Awake()
    {
        if(instance) Destroy(this);
        instance = this;
        
        gameObject.SetActive(false);
    }

    public void ToggleFPSText(bool state)
    {
        FPSText.enabled = state;
    }
    
    public void ToggleSpeedText(bool state)
    {
        SpeedTxt.enabled = state;
    }
    
}
