using System;
using Management.Settings;
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
            //This can never throw an error as this script requires a UI element
            if (TryGetComponent(out ui))
                ui.OnValueChanged += OverrideSetting;
            else
                print("Something has gone horribly, horribly wrong if you are seeing this");
        }

        private void OnEnable()
        {
            Setting s = new Setting {type = type, value = 0};
            var val = SettingsManager.GetSetting(s.type.ToString());
            print(val);
            //If we already have a saved version of this setting then use the value saved.
            setting = val != 0 ? new Setting {type = type, value = val} : new Setting {type = type, value = 0};
            SettingsManager.settings[(int)setting.type] = setting;
            SettingsManager.SaveSetting(setting);
            ui.currentIndex = (uint)setting.value;

            enabled = false;
        }

        public void OverrideSetting()
        {
            setting = new Setting {type = type, value = ui.currentIndex};
            SettingsManager.settings[(int)setting.type] = setting;
        }

        public Setting GetSetting()
        {
            return setting;
        }
    }
}