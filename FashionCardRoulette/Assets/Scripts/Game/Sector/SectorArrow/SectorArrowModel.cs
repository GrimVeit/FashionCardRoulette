using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorArrowModel
{
    public void ActivateArrowMove()
    {
        OnActivateArrowMove?.Invoke();
    }

    public void DeactivateArrowMove()
    {
        OnDeactivateArrowMove?.Invoke();
    }

    #region Output

    public event Action OnActivateArrowMove;
    public event Action OnDeactivateArrowMove;

    #endregion
}
