using System;
using UnityEngine;

namespace Management
{
    public class EventHandlerSystem : MonoBehaviour
    {
        public static EventHandlerSystem instance;
        private void Awake()
        {
            if(instance != null) Destroy(this);
            instance = this;
        }
        
        public event Action OnPause;
        public void Pause()
        {
            if (OnPause != null)
                OnPause();
        }
        
        public event Action OnUnpause;

        public void Unpause()
        {
            if (OnUnpause != null)
                OnUnpause();
        }
    }
}