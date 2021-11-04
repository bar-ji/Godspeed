using System;
using Management;
using TMPro;
using UnityEngine;

public class DebugMenu : MonoBehaviour
{
    public static DebugMenu instance;
    
    public TMP_Text speedTxt;
    public TMP_Text FPSTxt;
    public TMP_Text VersionTxt;

    private float timer;

    
    void Awake()
    {
        if(instance) Destroy(this);
        instance = this;
        
        gameObject.SetActive(false);
    }

    private void Start()
    {
        VersionTxt.text = "Version: " + GameManager.gameVersion;
    }

    void Update()
    {
        if (timer > 0.5f)
        {
            FPSTxt.text = "FPS: " + (1 / Time.unscaledDeltaTime).ToString("F0");
            timer = 0;
        }
        timer += Time.deltaTime;
    }
}
