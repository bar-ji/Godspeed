using System;
using UnityEngine;

namespace Management
{
    public class GameState : MonoBehaviour
    {
        public bool isPaused { get; private set;}

        public static GameState instance;

        private void Awake()
        {
            if (instance) Destroy(this);
            instance = this;
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                PauseMenu pauseMenu = PauseMenu.instance;

                if (pauseMenu.GetIsPaused())
                {
                    pauseMenu.OnUnpause.Invoke();
                }
                else
                {
                    pauseMenu.OnPause.Invoke();
                }
                   
            }
        }
    }
}