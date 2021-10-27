using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Checkbox : MonoBehaviour, ISettingUI
    {
        public int currentIndex { get; set; }
        public Action OnValueChanged { get; set; }

        [SerializeField] private Image circleFill;

        void Awake()
        {
            OnValueChanged += UpdateGraphic;
            UpdateGraphic();
        }

        public void FlipState()
        {
            if (currentIndex == 0)
                currentIndex = 1;
            else
                currentIndex = 0;

            OnValueChanged();

            UpdateGraphic();
        }

        void UpdateGraphic()
        {
            circleFill.enabled = currentIndex != 0;
        }
    }
}
