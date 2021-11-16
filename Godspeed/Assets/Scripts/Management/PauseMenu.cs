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
    private float[] pauseContentsXPosOnStart = new float[3];
    private PauseState currentState;

    private GameManager gameManager;

    //Menu State
    public bool isPaused { get; private set; }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
        
        gameManager = GameManager.instance;
        gameManager.pauseMenu = this;

        for (int i = 0; i < pauseContents.Length; i++)
        {
            pauseContentsXPosOnStart[i] = pauseContents[i].transform.localPosition.x;
            pauseContents[i].GetComponent<CanvasGroup>().alpha = 0;
        }
    }

    void Start()
    {
        gameManager.OnPause += OnPaused;
        gameManager.OnUnpause += OnUnpaused;

        //Initialise the state to be unpaused
        OnUnpaused();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                gameManager.Unpause();
            else
                gameManager.Pause();
        }
    }

    public void OnChangeState(PauseState state)
    {
        currentState = state;
        DisableContent();
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
            if (i == (int) state)
            {
                //Animate Alpha and disable clicking
                var canvasGroup = pauseContents[i].GetComponent<CanvasGroup>();
                canvasGroup.blocksRaycasts = true;
                canvasGroup.DOFade(1, 1f).SetEase(Ease.OutExpo);
                
                //Animate In
                var pos = pauseContentsXPosOnStart[i] - 50;
                canvasGroup.transform.DOLocalMoveX(pos, 1f).SetEase(Ease.OutExpo);
            }
        }
    }

    void DisableContent()
    {
        for (int i = 0; i < sizeof(PauseState) - 1; i++)
        {
            var canvasGroup = pauseContents[i].GetComponent<CanvasGroup>();
            canvasGroup.blocksRaycasts = false;
            canvasGroup.DOFade(0, 1f).SetEase(Ease.OutExpo);
            
            //Animate Out
            var pos = pauseContentsXPosOnStart[i] + 50;
            canvasGroup.transform.DOLocalMoveX(pos, 1f).SetEase(Ease.OutExpo);
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
        DisableContent();
        SetButtonState(PauseState.None);
    }
}
