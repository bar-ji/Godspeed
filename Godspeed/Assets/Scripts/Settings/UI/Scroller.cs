using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class Scroller : MonoBehaviour, ISettingUI
    {
        public int currentIndex { get; set; }
        private int maxIndex;
        public Action OnValueChanged { get; set; }
        [SerializeField] private TMP_Text text;
        [SerializeField] private List<string> valueFields = new List<string>();

        private void Awake()
        {
            maxIndex = valueFields.Count-1;
            OnValueChanged += UpdateText;
            UpdateText();
        }

        public void Increment()
        {
            if (currentIndex < maxIndex)
            {
                currentIndex++;
                OnValueChanged();
            }
        }
    
        public void Decrement()
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                OnValueChanged();
            }
        }

        public void UpdateText()
        {
            text.text = valueFields[(int)currentIndex];
        }
    }
}   
