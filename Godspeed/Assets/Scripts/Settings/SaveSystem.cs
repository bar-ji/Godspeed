using System;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//Handles Saving to disk
namespace Settings
{
    public static class SaveSystem
    {
        private static bool isFirstLoad;
        
        public static void SaveInt(string filename, IntData data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            
            string path = Application.persistentDataPath + "/" + filename + ".save";
            FileStream stream = new FileStream(path, FileMode.Create);

            formatter.Serialize(stream, data);
            stream.Close();

            Debug.Log("Saved: " + filename + "->" + data.value);
        }

        public static IntData LoadInt(string filename)
        {
            string path = Application.persistentDataPath + "/" + filename + ".save";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                IntData data = formatter.Deserialize(stream) as IntData;

                stream.Close();
                Debug.Log("Loaded: " + filename + "->" + data.value);
                return data;
            }
            
            //We know that this is the first boot because no data was found.
            Debug.Log("First Boot ");
            isFirstLoad = true;
            
            //Loop from after NONE through Enum and save files.
            int _default = 0;
            SettingsManager.instance.SendMessage("Save" + filename, _default);
            return new IntData(0);
        }
    }
    
    [Serializable]
    public class IntData
    {
        public int value;

        public IntData(int value)
        {
            this.value = value;
        }
    }
}
