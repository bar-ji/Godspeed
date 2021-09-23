using System;
using System.Collections;
using System.Collections.Generic;
using Management;
using UnityEngine;


public enum PauseState
{
    Video, Audio
}

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    public PauseState currentState { get; private set; }

    [SerializeField] private List<PauseButton> buttons;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    public void OnChangeState(PauseState state)
    {
        currentState = state;
        for(int i = 0; i < buttons.Count; i++)
        {
            if (i == (int)state)
            {
                buttons[i].OnSelected();
            }
            else
                buttons[i].OnDeselected();
        }
    }
}
