using System;

public interface ISettingUI
{
    public Action OnValueChanged { get; set; }
    public int currentIndex { get; set; }
}
