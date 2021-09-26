using System;
using UnityEngine;

namespace Management
{
    public class GameState : MonoBehaviour
    {
        [HideInInspector] public bool isPaused;
        
        public static GameState instance;

        private void Awake()
        {
            if (instance) Destroy(this);
            instance = this;
        }
    }
}