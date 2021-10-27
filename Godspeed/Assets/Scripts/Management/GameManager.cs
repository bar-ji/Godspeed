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

        private void Awake()
        {
            gameVersion = Application.version;
        }

        private void Start()
        {
            if (instance) Destroy(this);
            instance = this;

            DOTween.Init();

            actionManager.OnPause += OnPauseStateChanged;
            actionManager.OnUnpause += OnPauseStateChanged;
        }

        private void OnPauseStateChanged()
        {
            cameraController.OnPauseStateChanged();
        }
    }
}