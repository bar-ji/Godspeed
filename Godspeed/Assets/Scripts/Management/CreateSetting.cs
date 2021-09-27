using System;
using UnityEngine;

namespace Management
{
    public class CreateSetting : MonoBehaviour
    {
        [SerializeField] private SettingType type;
        [SerializeField] private float value;
        //TODO - THIS
        private void Start()
        {
            var val = SettingsManager.GetSetting(new Setting {type = type, value = 0});
            Setting setting;
            //If we already have a saved version of this setting then use the value saved.
            setting = val == value ? new Setting {type = type, value = value} : new Setting {type = type, value = val};
            SettingsManager.settings[(int)setting.type] = setting;
            SettingsManager.ApplySetting(setting);
            
            print(setting.type + " " + SettingsManager.GetSetting(setting));
            
            enabled = false;
        }
    }
}