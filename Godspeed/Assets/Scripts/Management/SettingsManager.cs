using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Management
{
    public class SettingsManager : MonoBehaviour
    {
        public static SettingsManager manager;

        public static Setting[] settings = new Setting[sizeof(SettingType)];

        private void Awake()
        {
            if(manager) Destroy(this);
            manager = this;
        }
        public static void ApplyAllSettings()
        {
            foreach (Setting setting in settings)
            {
                if(setting != null)
                    Saver.SaveValue(setting);
            }
        }
        public static void ApplySetting(Setting setting)
        {
            Saver.SaveValue(setting);
        }
        
        public static void ResetAllSettings()
        {
            foreach (Setting setting in settings)
            {
                setting.value = 0;
                Saver.SaveValue(setting);
            }
        }
        
        public static void DeleteAllSettings()
        {
            PlayerPrefs.DeleteAll();
        }
        
        public static float GetSetting(Setting setting) => Saver.GetValue(setting);
    }

    public class Setting
    {
        public SettingType type { get; set; }
        public float value { get; set; }
    }
    
    //Update this to add new setting. Add CreateSetting Script to any object.
    public enum SettingType
    {
        resolution, IsFullscreen
    }

    //Resolution: 0 - 1280x720, 1 - 1920x1080, 2 - 2560x1440, 3 - 4096x2160
    //IsFullscreen: 0 - false, 1 - true

}

