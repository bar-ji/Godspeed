using System;
using System.Collections;
using System.Collections.Generic;
using Management;
using UnityEngine;

public class Scroller : MonoBehaviour, ISettingUI
{
    public uint currentIndex { get; set; }
    [SerializeField] private uint maxIndex;

    public event Action OnValueChanged;
    
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
}
