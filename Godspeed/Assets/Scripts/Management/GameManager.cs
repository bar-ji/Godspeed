using System;
using Cam;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Management
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance { get; private set; }
        
        public InputManager InputManager;
        [HideInInspector] public PauseMenu pauseMenu;
        
        public event Action OnPause;
        public event Action OnUnpause;
        
        [SerializeField] private PostProcessProfile postProcessProfile;
        private DepthOfField dof;

        public static string gameVersion;

        private void Awake()
        {
            if (instance) Destroy(this);
            instance = this;
            
            gameVersion = Application.version;
            
            postProcessProfile.TryGetSettings(out dof);
        }

        private void Start()
        {
            DOTween.Init();

            OnPause += InputManager.instance.PauseInput;
            OnUnpause += InputManager.instance.UnpauseInput;

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
        
        public void OnPauseStateChanged()
        {
            ManageDOF();
        }
        
        private void ManageDOF()
        {
            if(!dof) return;
            
            dof.enabled.value = !dof.enabled.value;
            dof.focalLength.value = 30;
            if (dof.enabled.value)
                DOTween.To(() => dof.focalLength.value, x => dof.focalLength.value = x, 60f, 1f).SetEase(Ease.OutBack);
        }

        void OnApplicationQuit()
        {
            dof.enabled.value = false;
        }
    }
}