using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TaskVisualCell : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public NumberValue CurrentNumberValue => _currentNumberValue;
    public bool IsHaveNumber => _currentNumberValue != null;
    public int Id => id;

    [SerializeField] private int id;
    [SerializeField] private TextMeshProUGUI textNumber;

    [SerializeField] private Transform transformCell;
    [SerializeField] private Image imageHighlight;
    [SerializeField] private Image imageHighlight_Win;

    private NumberValue _currentNumberValue;
    private bool isActive;
    private bool isActivaWin;

    private readonly float scaleMin = 1f;
    private readonly float scaleMax = 1.1f;
    private readonly float pulseDuration = 0.4f;
    private readonly float deactivateDuration = 0.2f;

    private Sequence sequencePulse;

    public void SetData(NumberValue numberValue, Color color)
    {
        Deactivate();

        _currentNumberValue = numberValue;

        textNumber.text = _currentNumberValue.Number.ToString();
        textNumber.color = color;
    }

    public void Activate()
    {
        if(IsHaveNumber) return;

        isActive = true;

        sequencePulse?.Kill();

        transformCell.localScale = Vector3.one * scaleMin;
        imageHighlight.color = new Color(imageHighlight.color.r, imageHighlight.color.g, imageHighlight.color.b, 0);

        sequencePulse = DOTween.Sequence();

        sequencePulse
            .Append(transformCell.DOScale(scaleMax, pulseDuration))
            .Join(imageHighlight.DOFade(1, pulseDuration))
            .Append(transformCell.DOScale(scaleMin, pulseDuration))
            .Join(imageHighlight.DOFade(0, pulseDuration))
            .SetLoops(-1);
    }

    public void Deactivate()
    {
        if(IsHaveNumber) return;

        isActive = false;

        sequencePulse?.Kill();

        transformCell.DOScale(scaleMin, deactivateDuration);
        imageHighlight.DOFade(0, deactivateDuration);
    }

    public void ActivateWin()
    {
        imageHighlight_Win.DOFade(1, deactivateDuration);
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (IsHaveNumber || !isActive) return;

        OnChoose?.Invoke(id);
    }

    public event Action<int> OnChoose;
}
