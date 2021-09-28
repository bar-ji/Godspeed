using System.Collections;
using System.Collections.Generic;
using Management;
using UI;
using UnityEngine;

public class ApplySettings : MonoBehaviour
{
    public void ApplyAllSettings()
    {
        SettingsManager.ApplyAllSettings();
    }
}
