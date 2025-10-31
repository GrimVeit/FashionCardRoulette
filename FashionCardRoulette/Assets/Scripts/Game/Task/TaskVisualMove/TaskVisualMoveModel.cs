using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskVisualMoveModel
{
    public void MoveFinish()
    {
        OnMoveFinish?.Invoke();
    }

    #region Output

    public event Action OnMoveFinish;

    #endregion
}
