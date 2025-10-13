using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToggleCustom : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int Id => id;

    [SerializeField] private int id;
    [SerializeField] private Transform transformHandle;
    [SerializeField] private float durationChange;

    private Tween tweenScale;

    public void Activate()
    {
        tweenScale?.Kill();

        tweenScale = transformHandle.DOScale(1, durationChange);
    }

    public void Deactivate()
    {
        tweenScale?.Kill();

        tweenScale = transformHandle.DOScale(0, durationChange);
    }

    public void SetData(int id)
    {
        this.id = id;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnChooseToggle?.Invoke(id);
    }

    #region Output

    public event Action<int> OnChooseToggle;

    #endregion
}
