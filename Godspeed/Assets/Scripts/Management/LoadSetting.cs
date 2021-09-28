using System;
using UnityEngine;

namespace Management
{
    [RequireComponent(typeof(ISettingUI))]
    public class LoadSetting : MonoBehaviour
    {
        [SerializeField] private SettingType type;
        private Setting setting;
        private ISettingUI ui;
        
        private void Awake()
        {
            if (TryGetComponent(out ui))
            {
                ui.OnValueChanged += OverrideSetting;
                print("Found UI Element...");
            }
        }

        private void OnEnable()
        {
            var val = SettingsManager.GetSetting(new Setting {type = type, value = 0});
            //If we already have a saved version of this setting then use the value saved.
            setting = val != 0 ? new Setting {type = type, value = val} : new Setting {type = type, value = 0};
            SettingsManager.settings[(int)setting.type] = setting;
            SettingsManager.ApplySetting(setting);
            ui.currentIndex = (uint)setting.value;
            print("Initial Value: " + setting.value);

            enabled = false;
        }

        public void OverrideSetting()
        {
            setting = new Setting {type = type, value = ui.currentIndex};
            SettingsManager.settings[(int)setting.type] = setting;
            
            print(setting.type + " " + setting.value);
        }

        public Setting GetSetting()
        {
            return setting;
        }
    }
}