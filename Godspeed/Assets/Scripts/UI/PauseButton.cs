using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UI;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;

public class PauseButton : MonoBehaviour, IButton
{
    [SerializeField] private Transform borderImage;

    [SerializeField] private Vector2 startScale;
    [SerializeField] private Vector2 endScale;

    [SerializeField] private float scaleTime;

    public bool isSelected { get; set; }

    [SerializeField] private PauseState state;


    public void OnCursorEnter()
    {
        transform.DOPunchScale(new Vector3(0.02f, 0.02f, 0.02f), scaleTime, 10, 0.1f);
        if(!isSelected)
            ScaleUp();
    }

    public void OnCursorExit()
    {
        if(!isSelected)
            ScaleDown();
    }

    public void OnClick()
    {
        transform.DOPunchScale(new Vector3(0.05f, 0.05f, 0.05f), scaleTime, 10, 0.1f);
        PauseMenu.instance.OnChangeState(state);
    }

    public void OnSelected()
    {
        isSelected = true;
        ScaleUp();
    }

    public void OnDeselected()
    {
        isSelected = false;
        ScaleDown();
    }

    void ScaleUp()
    {
        borderImage.DOScale(endScale, scaleTime);
    }

    void ScaleDown()
    {
        borderImage.DOScale(startScale, scaleTime);
    }
}
