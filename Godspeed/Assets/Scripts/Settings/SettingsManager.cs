using UnityEngine;


//Singleton for interfacing with the SaveSystem

namespace Settings
{
    public class SettingsManager : MonoBehaviour
    {
        public static SettingsManager instance { get; set; }
        private void Awake()
        {
            if(instance) Destroy(this);
            instance = this;
        }

        public void SaveResolution(int value)
        {
            IntData data = new IntData(value);
            SaveSystem.SaveInt("Resolution", data);
            SettingFunctionality.SetResolution(data.value);
        }

        public void SaveFullscreen(int value)
        {
            IntData data = new IntData(value);
            SaveSystem.SaveInt("Fullscreen", data);
            SettingFunctionality.SetFullscreen(data.value);
        }

        public int LoadSetting(SettingType settingType)
        {
            int val;
            switch (settingType)
            {
                case SettingType.Resolution:
                    val = SaveSystem.LoadInt("Resolution").value;
                    SettingFunctionality.SetResolution(val);
                    return val;
                case SettingType.Fullscreen:
                    val = SaveSystem.LoadInt("Fullscreen").value;
                    SettingFunctionality.SetFullscreen(val);
                    return val;
            }
            return -1;
        }
    }

    public enum SettingType
    {
        None, Resolution, Fullscreen
    }

//HOW TO ADD NEW SETTING
//1. ADD THE NAME OF THE SETTING TO ENUM
//2. ADD A METHOD WITH THE SAME NAME IN THIS CLASS, TAKES IN AN INT.
//3. CREATE ONE OF THE ISettingUI ELEMENTS AND CHOOSE THAT ENUM.
//4. ADD FUNCTIONALITY IN SETTINGSFUNCTIONALITY
// DONE :)
}