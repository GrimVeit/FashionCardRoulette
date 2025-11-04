using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SectorZone : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
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

        OnClickToZone?.Invoke();
    }

    #region Output

    public event Action OnClickToZone;

    #endregion
}
