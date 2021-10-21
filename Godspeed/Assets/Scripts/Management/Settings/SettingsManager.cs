using UnityEngine;

namespace Management.Settings
{
    public class SettingsManager : MonoBehaviour
    {
        public static Setting[] settings = new Setting[sizeof(SettingType)];

        public GameManager gameManager => GameManager.instance;

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
                            print("Set 'Screen Resolution' to: 1280x720");
                            break;
                        case 1:
                            Screen.SetResolution(1920, 1080, isFullscreen);
                            print("Set 'Screen Resolution' to: 1920x1080");
                            break;
                        case 2:
                            Screen.SetResolution(2560, 1440, isFullscreen);
                            print("Set 'Screen Resolution' to: 2560x1440");
                            break;
                        case 3:
                            Screen.SetResolution(3840, 2160, isFullscreen);
                            print("Set 'Screen Resolution' to: 3850x2160");
                            break;
                    }
                    break;
                case SettingType.IsFullscreen:
                    switch (setting.value)
                    {
                        case 0:
                            Screen.fullScreen = false;
                            print("Set 'Fullscreen State' to: False");
                            break;
                        case 1:
                            Screen.fullScreen = true;
                            print("Set 'Fullscreen State' to: True");
                            break;
                    }
                    break;
                case SettingType.ShowFPS:
                    switch (setting.value)
                    {
                        case 0:
                            DebugMenu.instance.ToggleFPSText(false);
                            print("Set 'Show FPS' to: False");
                            break;
                        case 1:
                            DebugMenu.instance.ToggleFPSText(true);
                            print("Set 'Show FPS' to: True");
                            break;
                    }
                    break;
                case SettingType.ShowSpeed:
                    switch (setting.value)
                    {
                        case 0:
                            DebugMenu.instance.ToggleSpeedText(false);
                            print("Set 'Show Speed' to: False");
                            break;
                        case 1:
                            DebugMenu.instance.ToggleSpeedText(true);
                            print("Set 'Show Speed' to: True");
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
    
    //How to add Setting
    //1. Update this enum
    //2. Add functionality in ApplySetting method
    //3. Add a "Load Setting" class onto a GameObject and select the correct setting.
    public enum SettingType
    {
        Resolution, IsFullscreen, ShowFPS, ShowSpeed
    }

    //Resolution: 0 - 1280x720, 1 - 1920x1080, 2 - 2560x1440, 3 - 3840x2160
    //IsFullscreen: 0 - false, 1 - true
    //ShowFPS: 0 - false, 1 - true

}

