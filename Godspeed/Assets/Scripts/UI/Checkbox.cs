using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkbox : MonoBehaviour, ISettingUI
{
    public uint currentIndex { get; set; }
    public event Action OnValueChanged;

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
