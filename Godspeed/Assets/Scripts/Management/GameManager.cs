using System;
using Cam;
using DG.Tweening;
using Management.Settings;
using UnityEngine;

namespace Management
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance { get; private set; }
        public CameraController cameraController;
        public EventHandlerSystem eventHandlerSystem;
        [HideInInspector] public PauseMenu pauseMenu;
        
        private void Start()
        {
            if (instance) Destroy(this);
            instance = this;

            DOTween.Init();

            eventHandlerSystem.OnPause += OnPauseStateChanged;
            eventHandlerSystem.OnUnpause += OnPauseStateChanged;
        }

        private void OnPauseStateChanged()
        {
            cameraController.OnPauseStateChanged();
        }
    }

    public enum GameState
    {
        PLAYING, PAUSED
    }
}