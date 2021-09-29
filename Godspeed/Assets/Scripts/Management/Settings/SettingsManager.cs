using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Management
{
    public class SettingsManager : MonoBehaviour
    {
        public static Setting[] settings = new Setting[sizeof(SettingType)];
        
        public static void SaveAllSettings()
        {
            foreach (Setting setting in settings)
            {
                if (setting != null)
                    Saver.SaveValue(setting);
                else return;

                ApplySetting(setting);
            }
        }
        public static void SaveSetting(Setting setting)
        {
            Saver.SaveValue(setting);
            ApplySetting(setting);
        }
        
        private static void ApplySetting(Setting setting)
        {
            switch (setting.type)
            {
                case SettingType.Resolution:
                    bool isFullscreen = Saver.GetValue(SettingType.IsFullscreen.ToString()) != 0;
                    switch (setting.value)
                    {
                        case 0:
                            Screen.SetResolution(1280, 720, isFullscreen);
                            break;
                        case 1:
                            Screen.SetResolution(1920, 1080, isFullscreen);
                            break;
                        case 2:
                            Screen.SetResolution(2560, 1440, isFullscreen);
                            break;
                        case 3:
                            Screen.SetResolution(3840, 2160, isFullscreen);
                            break;
                    }
                    break;
                case SettingType.IsFullscreen:
                    switch (setting.value)
                    {
                        case 0:
                            Screen.fullScreen = false;
                            break;
                        case 1:
                            Screen.fullScreen = true;
                            break;
                    }
                    break;
            }
            print("Settings applied successfully");
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
        
        public static float GetSetting(string setting) => Saver.GetValue(setting);
    }

    public class Setting
    {
        public SettingType type { get; set; }
        public float value { get; set; }
    }
    
    //Update this to add new setting. Add CreateSetting Script to any object.
    public enum SettingType
    {
        Resolution, IsFullscreen
    }

    //Resolution: 0 - 1280x720, 1 - 1920x1080, 2 - 2560x1440, 3 - 3840x2160
    //IsFullscreen: 0 - false, 1 - true

}

