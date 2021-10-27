using System;
using Cam;
using DG.Tweening;
using UnityEngine;

namespace Management
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance { get; private set; }
        
        public CameraController cameraController;
        public ActionManager actionManager;
        [HideInInspector] public PauseMenu pauseMenu;

        public static string gameVersion;
        public event Action OnPause;
        public event Action OnUnpause;

        private void Awake()
        {
            if (instance) Destroy(this);
            instance = this;
            
            gameVersion = Application.version;
        }

        private void Start()
        {
            DOTween.Init();

            OnPause += OnPauseStateChanged;
            OnUnpause += OnPauseStateChanged;
        }
        
        public void Pause()
        {
            if (OnPause != null)
                OnPause();
        }
        

        public void Unpause()
        {
            if (OnUnpause != null)
                OnUnpause();
        }
        

        private void OnPauseStateChanged()
        {
            cameraController.OnPauseStateChanged();
        }
    }
}