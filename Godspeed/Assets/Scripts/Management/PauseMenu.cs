using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Management;
using Scriptable_Objs;
using UnityEngine;
using EventHandler = System.EventHandler;


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

    [SerializeField] private GameObject[] pauseContents;
    private PauseState currentState;

    private EventHandlerSystem eventHandler;

    //Menu State
    public bool isPaused { get; private set; }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    void Start()
    {
        GameManager.instance.pauseMenu = this;
        
        eventHandler = EventHandlerSystem.instance;
        
        eventHandler.OnPause += OnPaused;
        eventHandler.OnUnpause += OnUnpaused;

        //Initialise the state to be unpaused
        OnUnpaused();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                eventHandler.Unpause();
            else
                eventHandler.Pause();
        }
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
                buttons[i].OnSelected();
            else
                buttons[i].OnDeselected();
        }
    }

    void SetContentState(PauseState state, bool activeState)
    {
        for (int i = 0; i < sizeof(PauseState) - 1; i++)
        {
            if (i == (int)state)
                pauseContents[i].SetActive(activeState);
        }
    }

    void DisableContent()
    {
        for (int i = 0; i < sizeof(PauseState) - 1; i++)
            pauseContents[i].SetActive(false);
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
}
