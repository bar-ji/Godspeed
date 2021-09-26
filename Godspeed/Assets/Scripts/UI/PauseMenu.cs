using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Management;
using Scriptable_Objs;
using UnityEngine;


public enum PauseState
{
    Video, Audio, Debug, None
}

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;

    //Nested Pause State
    [SerializeField] private List<PauseButton> buttons;

    [SerializeField] private MovableImageData sidebarData;
    [SerializeField] private Transform sidebar;

    [SerializeField] private GameObject video;
    [SerializeField] private new GameObject audio;
    [SerializeField] private GameObject debug;

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
    }

    public void OnChangeState(PauseState state)
    {
        SetContentState(currentState, false);
        currentState = state;
        SetContentState(currentState, true);
        
        SetButtonState(state);
    }

    void SetButtonState(PauseState state)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (i == (int) state)
            {
                buttons[i].OnSelected();
            }
            else
                buttons[i].OnDeselected();
        }
    }

    void SetContentState(PauseState state, bool activeState)
    {
        switch (state)
        {
            case PauseState.Video:
                video.SetActive(activeState);
                break;
            case PauseState.Audio:
                audio.SetActive(activeState);
                break;
            case PauseState.Debug:
                debug.SetActive(activeState);
                break;
        }
    }

    void DisableContent()
    {
        video.SetActive(false);
        audio.SetActive(false);
        debug.SetActive(false);
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
        DisableContent();
        SetButtonState(PauseState.None);
    }

    public void SetIsPaused(bool value) => isPaused = value;
    public bool GetIsPaused() => isPaused;
}
