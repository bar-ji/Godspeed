using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Management;
using Scriptable_Objs;
using UnityEngine;


public enum PauseState
{
    Video, Audio, Debug
}

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    
    //Nested Pause State
    [SerializeField] private List<PauseButton> buttons;
    [SerializeField] private MovableImageData sidebarData;
    [SerializeField] private Transform sidebar;
    private PauseState currentState;

    //Menu State
    private bool isPaused;
    public Action OnPause;
    public Action OnUnpause;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
        
        OnPause += OnPaused;
        OnUnpause += OnUnpaused;
        
        //Initialise the state to be unpaused
        OnUnpause();
        OnChangeState(PauseState.Video);
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

    void OnPaused()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
        sidebar.DOLocalMove(sidebarData.endPos, sidebarData.time).SetEase(sidebarData.easeType);
    }

    void OnUnpaused()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
        sidebar.DOLocalMove(sidebarData.startPos, sidebarData.time).SetEase(sidebarData.easeType);
    }

    public void SetIsPaused(bool value) => isPaused = value;
    public bool GetIsPaused() => isPaused;
}
