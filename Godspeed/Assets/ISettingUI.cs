using System;

public interface ISettingUI
{
    public event Action OnValueChanged;
    public uint currentIndex { get; set; }
}
