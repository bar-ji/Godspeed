using System;
using System.Collections;
using System.Collections.Generic;
using Management;
using TMPro;
using UnityEngine;

public class Scroller : MonoBehaviour, ISettingUI
{
    public uint currentIndex { get; set; }
    [SerializeField] private uint maxIndex;
    public event Action OnValueChanged;
    [SerializeField] private TMP_Text text;
    [SerializeField] private List<string> valueFields = new List<string>();

    private void Awake()
    {
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
