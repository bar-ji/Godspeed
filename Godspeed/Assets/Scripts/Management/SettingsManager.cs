using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Management
{
    public class SettingsManager : MonoBehaviour
    {
        public static void StoreIntValue(string settingName, int value) => PlayerPrefs.SetInt(settingName, value);
        public static void GetIntValue(string settingName) => PlayerPrefs.GetInt(settingName);
    
        public static void StoreFloatValue(string settingName, float value) => PlayerPrefs.SetFloat(settingName, value);
        public static void GetFloatValue(string settingName) => PlayerPrefs.GetFloat(settingName);

        public static void StoreStringValue(string settingName, string value) => PlayerPrefs.SetString(settingName, value);
        public static void GetStringValue(string settingName) => PlayerPrefs.GetString(settingName);

        /*
        public static void StoreValue<T>(string settingName, T settingValue)
        {
            System.Type type = typeof(T);
            
            if (type == typeof(int))
                PlayerPrefs.SetInt(settingName, Convert.ToInt32(settingValue));
            else if (type == typeof(float))
                PlayerPrefs.SetFloat(settingName, (float)Convert.ToDouble(settingValue));
            else if (type == typeof(string))
                PlayerPrefs.SetString(settingName, settingValue.ToString());
        }
        */
    }
}

