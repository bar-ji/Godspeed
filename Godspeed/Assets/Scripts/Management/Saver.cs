using UnityEngine;
using System;

namespace Management
{
    public class Saver : MonoBehaviour
    {
        public static void SaveValue(Setting setting) => PlayerPrefs.SetFloat(Enum.GetName(typeof(SettingType), setting.type), setting.value);
        public static float GetValue(Setting setting) => PlayerPrefs.GetFloat(Enum.GetName(typeof(SettingType), setting.type));
    }
}