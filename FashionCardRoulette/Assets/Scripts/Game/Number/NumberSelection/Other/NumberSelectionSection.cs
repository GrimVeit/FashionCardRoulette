using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NumberSelectionSection : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int Number => number;

    [SerializeField] private int number;

    private bool isActive = false;

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!isActive) return;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(!isActive) return;

        OnSelectSection?.Invoke(number);
    }

    #region Output

    public event Action<int> OnSelectSection;

    #endregion
}
