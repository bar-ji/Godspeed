using UnityEngine;
using System;
using Management.Settings;

namespace Management
{
    public class Saver : MonoBehaviour
    {
        public static void SaveValue(Setting setting) => PlayerPrefs.SetFloat(setting.type.ToString(), setting.value);
        public static float GetValue(string settingName) => PlayerPrefs.GetFloat(settingName);
    }
}