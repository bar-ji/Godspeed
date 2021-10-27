using UnityEngine;

namespace Settings
{
    //This will probably change project to project
    public static class SettingFunctionality
    {
        public static void SetResolution(int index)
        {
            bool isFullscreen = SaveSystem.LoadInt("Fullscreen").value != 0;
            switch (index)
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
        }

        public static void SetFullscreen(int index)
        {
            switch (index)
            {
                case 0:
                    Screen.fullScreenMode = FullScreenMode.Windowed;
                    break;
                case 1: 
                    Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                    break;
            }
        }
    }
}