using System;
using UnityEngine;

namespace Management
{
    public class ActionManager : MonoBehaviour
    {
        public static ActionManager instance;
        private void Awake()
        {
            if(instance != null) Destroy(this);
            instance = this;
        }
    }
}