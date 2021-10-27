using UnityEngine;

namespace Settings
{
    //Interfaces between UI and SettingManager
    
    [RequireComponent(typeof(ISettingUI))]
    public class Saver : MonoBehaviour
    {
        private SettingsManager settingsManager;
        [SerializeField] private SettingType settingType;
        private ISettingUI settingUI;
        private void Awake()
        {
            settingUI = GetComponent<ISettingUI>();
        }

        private void Start()
        {
            settingsManager = SettingsManager.instance;
            settingUI.currentIndex = SettingsManager.instance.LoadSetting(settingType);
            settingUI.OnValueChanged();
            settingUI.OnValueChanged += SaveCheck;
        }
        
        public void SaveCheck()
        {
            if (settingType == SettingType.None)
            {
                print("SettingType can not be None");
                return;
            }
            var value = settingUI.currentIndex;
            settingsManager.SendMessage("Save" + settingType, value);
        }
    }
}
