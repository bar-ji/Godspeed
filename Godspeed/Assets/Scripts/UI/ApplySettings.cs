using System.Collections;
using System.Collections.Generic;
using Management;
using Management.Settings;
using UI;
using UnityEngine;

public class ApplySettings : MonoBehaviour
{
    public void ApplyAllSettings()
    {
        SettingsManager.SaveAllSettings();
    }
}
