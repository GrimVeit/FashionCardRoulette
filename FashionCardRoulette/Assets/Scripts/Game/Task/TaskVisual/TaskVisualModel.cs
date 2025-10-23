using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskVisualModel
{
    public void ResetTasks()
    {
        OnResetTasks?.Invoke();
    }

    #region Output

    public event Action OnResetTasks;

    #endregion
}
